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

using Plexdata.ModelGenerator.Interfaces;
using System;
using System.IO;

namespace Plexdata.ModelGenerator.Extensions
{
    internal static class CodeExtension
    {
        #region Construction

        static CodeExtension()
        {
        }

        #endregion

        #region Public Methods

        public static void EnsureFolderExistence(this ICode source, String folder)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source instance must not be null.");
            }

            if (String.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentOutOfRangeException(nameof(folder), "Folder must not be null, empty, or contain only whitespaces.");
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static String EnsureUniqueFilename(this ICode source, String folder, String filename, Boolean overwrite)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source instance must not be null.");
            }

            if (String.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentOutOfRangeException(nameof(folder), "Folder must not be null, empty, or contain only whitespaces.");
            }

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentOutOfRangeException(nameof(filename), "Filename must not be null, empty, or contain only whitespaces.");
            }

            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Folder \"{folder}\" does not exist.");
            }

            if (overwrite)
            {
                return filename;
            }

            String extension = Path.GetExtension(filename);
            String indicator = Path.GetFileNameWithoutExtension(filename);

            if (indicator.LastIndexOf('-') >= 0)
            {
                indicator = indicator.Substring(0, indicator.LastIndexOf('-'));
            }

            if (!File.Exists(Path.Combine(folder, $"{indicator}{extension}")))
            {
                filename = $"{indicator}{extension}";
            }
            else if (Directory.GetFiles(folder, $"{indicator}*{extension}", SearchOption.TopDirectoryOnly).Length > 0)
            {
                Int32 number = 0;

                do
                {
                    filename = $"{indicator}-{++number}{extension}";
                }
                while (File.Exists(Path.Combine(folder, filename)));
            }

            return filename;
        }

        #endregion
    }
}
