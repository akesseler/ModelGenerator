/*
 * MIT License
 * 
 * Copyright (c) 2023 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Plexdata.ModelGenerator.Gui.Events;
using System;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui.Controls
{
    public class TextBoxEx : TextBox
    {
        #region Public Events

        public event EventHandler<ClipboardEventArgs> OnPaste;

        public event EventHandler<EventArgs> OnEditingChanged;

        #endregion

        #region Private Fields 

        private const Int32 WM_PASTE = 0x0302;

        private Boolean editing = false;

        #endregion

        #region Construction

        public TextBoxEx()
            : base()
        {
        }

        #endregion

        #region Public Properties

        public Boolean IsEditing
        {
            get
            {
                return this.editing;
            }
            set
            {
                if (this.editing != value)
                {
                    this.editing = value;
                    this.OnEditingChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Protected Methods

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WM_PASTE)
            {
                if (!this.IsEditing)
                {
                    try
                    {
                        this.OnPaste?.Invoke(this, new ClipboardEventArgs(Clipboard.GetText()));
                    }
                    catch { }

                    return;
                }
            }

            base.WndProc(ref message);
        }

        #endregion
    }
}
