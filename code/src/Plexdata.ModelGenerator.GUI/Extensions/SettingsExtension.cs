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

using Plexdata.ModelGenerator.Gui.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui.Extensions
{
    public static class SettingsExtension
    {
        #region Construction

        static SettingsExtension()
        {
        }

        #endregion

        #region Public Methods

        public static void EnsureDisplayAttributes(this WindowSettings settings, Form window)
        {
            settings.ValidateArguments(window);

            Rectangle bounds = new Rectangle(settings.Location.ToPoint(), settings.Dimension.ToSize());

            if (settings.IsVisibleOnAllScreens(bounds))
            {
                window.StartPosition = FormStartPosition.Manual;
                window.DesktopBounds = bounds;
            }
            else
            {
                window.StartPosition = FormStartPosition.WindowsDefaultLocation;
                window.Size = bounds.Size;
            }
        }

        public static void ApplyLocation(this WindowSettings settings, Form window)
        {
            settings.ValidateArguments(window);

            if (window.WindowState == FormWindowState.Normal)
            {
                settings.Location.FromPoint(window.DesktopBounds.Location);
            }
        }

        public static void ApplyDimension(this WindowSettings settings, Form window)
        {
            settings.ValidateArguments(window);

            if (window.WindowState == FormWindowState.Normal)
            {
                settings.Dimension.FromSize(window.DesktopBounds.Size);
            }
        }

        #endregion

        #region Private Methods

        private static Boolean IsVisibleOnAllScreens(this WindowSettings settings, Rectangle bounds)
        {
            if (bounds.IsEmpty || bounds.Size.IsEmpty)
            {
                return false;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(bounds))
                {
                    return true;
                }
            }

            return false;
        }

        private static void ValidateArguments(this WindowSettings settings, Form window)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }
        }

        #endregion
    }
}
