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

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Plexdata.ModelGenerator.Gui.Extensions
{
    public static class FormExtension
    {
        public static void ShowHtml(this Form parent, String name, String html)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(html))
                {
                    return;
                }

                String path = Path.Combine(Path.GetTempPath(), $"{name}.html");

                using (FileStream stream = File.OpenWrite(path))
                {
                    using (TextWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(html);
                    }
                }

                Process.Start(path);
            }
            catch (Exception exception)
            {
                parent.ShowError("Sorry but the help cannot be opened in the default web browser.", exception);
            }
        }

        public static void ShowError(this Form parent, String message)
        {
            if (parent == null || String.IsNullOrWhiteSpace(message))
            {
                return;
            }

            String caption = String.IsNullOrWhiteSpace(parent.Text) ? "Error" : parent.Text;

            MessageBox.Show(parent, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError(this Form parent, Exception exception)
        {
            parent.ShowError(exception?.Message);
        }

        public static void ShowError(this Form parent, String message, Exception exception)
        {
            parent.ShowError($"{message}{Environment.NewLine}{Environment.NewLine}{exception?.Message}");
        }
    }
}
