﻿using BlazeCards.Client.Cards.Components;
using BlazeCards.Client.Cards.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeCards.Client.Cards.Behaviors
{
    public class TextBehavior
    {
        public TextComponent Card { get; private set; }

        public string Value { get; set; }
        public float Caret { get; set; }
        public bool Editing { get; set; }

        public TextBehavior()
        {
            this.Value = "default text";
        }

        public void AssignComponent(TextComponent card)
        {
            this.Card = card;
        }

        public void Focus()
        {
            this.Card.Canvas.JSRuntime.InvokeVoidAsync("setFocus", this.Card.TextRef);
        }

        public void KeyDown(KeyboardEventArgs e)
        {
            if (!this.Editing) return;

            if (e.Key.ToLower() == "backspace")
            {
                this.Value = this.Value.Length <= 0 ? "" : this.Value.Substring(0, this.Value.Length - 1);
                return;
            }

            if (e.Key.ToString().Length > 1) return;

            this.Value += e.Key.ToString();
        }

        public async Task GetCaretAsync()
        {
            float res = await this.Card.Canvas.JSRuntime.InvokeAsync<float>("getTextWidth", this.Card.TextRef);
            if (res != this.Caret)
            {
                this.Caret = res;
                this.Card.InvokeChange();
            }
        }

        public async Task<Vector2f> GetSize()
        {
            var size = new Vector2f();

            size.X = await this.Card.Canvas.JSRuntime.InvokeAsync<float>("getTextWidth", this.Card.TextRef);
            size.Y = await this.Card.Canvas.JSRuntime.InvokeAsync<float>("getTextHeight", this.Card.TextRef);

            return size;
        }
    }
}
