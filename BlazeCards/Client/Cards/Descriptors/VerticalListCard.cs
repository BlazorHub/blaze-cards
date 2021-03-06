﻿using BlazeCards.Client.Cards.Components;
using BlazeCards.Client.Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeCards.Client.Cards.Descriptors
{
    public class VerticalListCard : Card
    {
        public override Vector2f GetSize()
        {
            var size = Vector2f.Zero;

            foreach (var child in this.Children)
            {
                var childSize = child.GetSize();
                size.Y += childSize.Y;

                if (size.X > childSize.X)
                    size.X = childSize.X;
            }

            return size;
        }

        public override void Snap()
        {
            base.Snap();

            Console.WriteLine($"List snapping {this.Children.Count} children...");

            var offset = 0f;
            foreach(var child in this.Children)
            {
                child.PositionBehavior.Position = new Vector2f(0, offset);
                offset += child.GetSize().Y;
            }
        }

        public override Type GetComponentType() => typeof(ListComponent);
    }
}
