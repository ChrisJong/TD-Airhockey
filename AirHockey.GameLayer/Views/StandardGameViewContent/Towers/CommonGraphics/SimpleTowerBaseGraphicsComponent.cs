namespace AirHockey.GameLayer.Views.StandardGameViewContent.Towers.CommonGraphics
{
    using ComponentModel;
    using ComponentModel.Graphics;
    using Constants;
    using Resources;
    using AirHockey.InteractionLayer.Components;
    using AirHockey.Utility.Classes;
    using System;
    using System.Collections.Generic;

    class SimpleTowerBaseGraphicsComponent : GraphicsComponent
    {
        protected AnimationGraphicsComponent TagGraphic;
        protected AnimationGraphicsComponent ToggleRingGraphic;
        protected AnimationGraphicsComponent ToggleRingActivationGraphic;
        protected AnimationGraphicsComponent ActiveAnchorGraphic;
        protected AnimationGraphicsComponent EnergyGraphic;
        protected AnimationGraphicsComponent EnergyEmptyGraphic;
        protected AnimationGraphicsComponent RegenGraphic;
        protected AnimationGraphicsComponent ToggleCooldownGraphic;
        protected List<AnimationGraphicsComponent> GraphicList = new List<AnimationGraphicsComponent>();

        protected TowerObjectBase MyTower
        {
            get;
            set;
        }

        public SimpleTowerBaseGraphicsComponent(AnimationValues animationValues, GameObjectBase parentNode, params IMessageHandler[] messageHandlers)
            : base(parentNode, messageHandlers)
        {
            this.MyTower = (TowerObjectBase)parentNode;
            this.InitBaseGraphics(animationValues);
        }

        protected void InitBaseGraphics(AnimationValues animationValues)
        {
            //Tower TagIcon
            var resource = this.SendMessage<ResourceName>("Resource", animationValues.PlayerDirectory + ".TagIcon");
            this.TagGraphic = new AnimationGraphicsComponent(resource,
                animationValues.TagIconFrameCount,
                animationValues.TagIconFrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.TagIconDepth
            };
            this.GraphicList.Add(TagGraphic);


            //Tower ToggleRing
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Player" + MyTower.Player + ".ToggleRing");
            this.ToggleRingGraphic = new AnimationGraphicsComponent(resource,
                animationValues.ToggleRingFrameCount,
                animationValues.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.ToggleRingDepth
            };
            this.GraphicList.Add(ToggleRingGraphic);

            
            //Toggle Ring Activation
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Towers.ToggleRingActivation");
            this.ToggleRingActivationGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Default.ToggleRingActivatedFrameCount,
                animationValues.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.ToggleRingDepth,
                InFrame = AnimationValues.Default.ToggleRingActivatedInFrame
            };
            this.GraphicList.Add(ToggleRingActivationGraphic);


            // ToggleCooldown 
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Towers.ToggleRingCooldown");
            this.ToggleCooldownGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.Default.ToggleRingCooldownFrameCount,
                animationValues.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                AnimationPaused = true,
                DrawDepth = animationValues.ToggleRingDepth
            };
            this.GraphicList.Add(ToggleCooldownGraphic);


            //Tower ActiveAnchor
            resource = this.SendMessage<ResourceName>("Resource", animationValues.BaseDirectory + ".ActiveAnchor");
            this.ActiveAnchorGraphic = new AnimationGraphicsComponent(resource,
                animationValues.ActiveAnchorFrameCount,
                animationValues.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.ActiveAnchorDepth
            };
            this.GraphicList.Add(ActiveAnchorGraphic);


            // Tower EnergyRing
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Towers.EnergyRing");
            this.EnergyGraphic = new TowerEnergyAnimationComponent(resource,
                animationValues, 
                this.ParentNode, 
                this.MessageHandlers.ToArray());
            // Empty Ring
            this.GraphicList.Add(EnergyGraphic);

            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Towers.EnergyRingEmpty");
            this.EnergyEmptyGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.EnergyRing.TowerEnergyEmptyFramecount,
                AnimationValues.Default.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
                {
                    DrawDepth = animationValues.EnergyRingDepth
                };
            this.GraphicList.Add(EnergyEmptyGraphic);

            // Regen Effect
            resource = this.SendMessage<ResourceName>("Resource", "Resources.<skin>.Towers.TowerRegen");
            this.RegenGraphic = new AnimationGraphicsComponent(resource,
                AnimationValues.EnergyRing.TowerRegenFrameCount,
                AnimationValues.Default.FrameDuration,
                this.ParentNode,
                this.MessageHandlers.ToArray())
            {
                DrawDepth = animationValues.EnergyRingDepth - 0.01f,
                Alpha = 0
            };
            this.GraphicList.Add(RegenGraphic);
        }

        public override void Update(double delta)
        {
            this.RenderRotationOffset = -this.MyTower.Physics.Rotation; //Zero out the rotation

            if (this.MyTower.Player == Player.One)
            {
                this.RenderRotationOffset += (float)(Math.PI * 0.5f); // +90 degrees.
            }
            else
            {
                this.RenderRotationOffset += (float)(Math.PI * 1.5f); // + 270 degrees.
            }

            GraphicList.ForEach(g => { g.Update(delta); g.RenderRotationOffset = this.RenderRotationOffset; });
            base.Update(delta);
        }

        public override void Draw()
        {
            if (this.ParentNode.IsActive || MyTower.ToggleCooldown > 0 || MyTower.TaglessCooldown > 0)
            {
                SetTaglessAlpha(this.TagGraphic);
                this.TagGraphic.Draw();

                // Energy Ring
                if (this.MyTower.Energy > 0.0f)
                {
                    SetTaglessAlpha(this.EnergyGraphic);
                    this.EnergyGraphic.CurrentFrame = (int)this.MyTower.Energy;
                    this.EnergyGraphic.Draw();
                }
                else
                {
                    SetTaglessAlpha(this.EnergyEmptyGraphic);
                    this.EnergyEmptyGraphic.Draw();
                }

                if (this.RegenGraphic.Alpha > 0.02f)
                {
                    this.RegenGraphic.Alpha -= this.RegenGraphic.Alpha / 15;
                    this.RegenGraphic.Draw();
                }


                // Not Activated and Not Pulled Back
                if (!this.MyTower.IsActivated && !this.MyTower.IsPulledBack)
                {
                    // Draws the ring if not out of energy
                    if (!this.MyTower.IsOutOfEnergy)
                    {
                        if (MyTower.ToggleCooldown > 0)
                        {
                            SetTaglessAlpha(this.ToggleCooldownGraphic);
                            this.ToggleCooldownGraphic.CurrentFrame = (int)(MyTower.ToggleCooldown / MyTower.ToggleCooldownMax * AnimationValues.Default.ToggleRingCooldownFrameCount);
                            this.ToggleCooldownGraphic.Draw();
                        }
                        else
                        {
                            SetTaglessAlpha(this.ToggleRingGraphic);
                            this.ToggleRingGraphic.Draw();
                        }
                    }
                    this.ToggleRingActivationGraphic.CurrentFrame = 1;

                    // Draws regen effect if not in use
                    if (this.MyTower.RecentRegen > 0)
                    {
                        this.RegenGraphic.Alpha += (1 - this.RegenGraphic.Alpha) / 5;
                    }
                }
                else
                {
                    // Draws ActiveAnchor
                    if (this.MyTower.PullBackPoint != null)
                    {
                        SetTaglessAlpha(this.ActiveAnchorGraphic);
                        SetTaglessAlpha(this.ToggleRingActivationGraphic);
                        this.ActiveAnchorGraphic.RenderPositionOffset = this.MyTower.PullBackPoint - this.MyTower.Physics.Position;
                        this.ActiveAnchorGraphic.RenderRotationOffset = this.MyTower.PullBackRotation;
                        this.ActiveAnchorGraphic.Draw();
                        this.ToggleRingActivationGraphic.Draw();
                    }
                }
            }
        }

        public override void Release()
        {
            this.MyTower = null;
            this.GraphicList.Clear();
            base.Release();
        }
    }
}
