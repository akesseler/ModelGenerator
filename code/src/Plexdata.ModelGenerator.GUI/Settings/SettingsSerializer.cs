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
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Plexdata.ModelGenerator.Gui.Settings
{
    public static class SettingsSerializer
    {
        #region Private Fields

        private static readonly String filename = String.Empty;

        #endregion

        #region Construction

        static SettingsSerializer()
        {
            SettingsSerializer.filename = SettingsSerializer.GetSettingsFilename();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the fully qualified path of the used default settings file.
        /// </summary>
        public static String Filename
        {
            get
            {
                return SettingsSerializer.filename;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Tries to load settings using default settings file.
        /// </summary>
        /// <remarks>
        /// This method tries to load settings using default settings file.
        /// </remarks>
        /// <typeparam name="TInstance">
        /// The type to load the settings file content into.
        /// </typeparam>
        /// <param name="instance">
        /// The resulting instance of the settings file content.
        /// </param>
        /// <returns>
        /// In case of an error the method returns false. Otherwise the method 
        /// returns true.
        /// </returns>
        public static Boolean Load<TInstance>(out TInstance instance) where TInstance : class, new()
        {
            return SettingsSerializer.Load(SettingsSerializer.Filename, out instance);
        }

        /// <summary>
        /// Tries to load settings using provided settings file.
        /// </summary>
        /// <remarks>
        /// This method tries to load settings using provided settings file.
        /// </remarks>
        /// <typeparam name="TInstance">
        /// The type to load the settings file content into.
        /// </typeparam>
        /// <param name="filename">
        /// The fully qualified name of the settings file.
        /// </param>
        /// <param name="instance">
        /// The resulting instance of the settings file content.
        /// </param>
        /// <returns>
        /// In case of an error the method returns false. Otherwise the method 
        /// returns true.
        /// </returns>
        public static Boolean Load<TInstance>(String filename, out TInstance instance) where TInstance : class, new()
        {
            instance = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TInstance));

                using (TextReader reader = new StreamReader(filename))
                {
                    instance = (TInstance)serializer.Deserialize(reader);

                    return true;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        /// <summary>
        /// Tries to save settings using default settings file.
        /// </summary>
        /// <remarks>
        /// This method tries to save settings using default settings file.
        /// </remarks>
        /// <typeparam name="TInstance">
        /// The type to save the settings file content from.
        /// </typeparam>
        /// <param name="instance">
        /// The instance of the settings.
        /// </param>
        /// <returns>
        /// In case of an error the method returns false. Otherwise the method 
        /// returns true.
        /// </returns>
        public static Boolean Save<TInstance>(TInstance instance) where TInstance : class, new()
        {
            return SettingsSerializer.Save(SettingsSerializer.Filename, instance);
        }

        /// <summary>
        /// Tries to save settings using provided settings file.
        /// </summary>
        /// <remarks>
        /// This method tries to save settings using provided settings file.
        /// </remarks>
        /// <typeparam name="TInstance">
        /// </typeparam>
        /// <param name="filename">
        /// The fully qualified name of the settings file.
        /// </param>
        /// <param name="instance">
        /// The instance of the settings.
        /// </param>
        /// <returns>
        /// In case of an error the method returns false. Otherwise the method 
        /// returns true.
        /// </returns>
        public static Boolean Save<TInstance>(String filename, TInstance instance) where TInstance : class, new()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TInstance));

                using (TextWriter writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, instance);
                }

                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the fully qualified settings file name by preferring path of 
        /// the executable. The path of user's local application data is returned 
        /// in case of missing write permission on preferred path.
        /// </summary>
        /// <returns>
        /// The fully qualified settings file name.
        /// </returns>
        private static String GetSettingsFilename()
        {
            String site = Path.GetFullPath(Application.ExecutablePath);
            String file = Path.ChangeExtension(Path.GetFileName(site), ".conf");
            String path = Path.GetDirectoryName(site);

            if (SettingsSerializer.IsAccessPermitted(path, FileSystemRights.Write))
            {
                return Path.Combine(path, file);
            }

            if (String.IsNullOrWhiteSpace(Application.CompanyName))
            {
                // May never happen, but safety first...
                throw new ArgumentException("Company name is invalid.");
            }

            if (String.IsNullOrWhiteSpace(Application.ProductName))
            {
                // May never happen, but safety first...
                throw new ArgumentException("Product name is invalid.");
            }

            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            path = Path.Combine(path, Application.CompanyName, Application.ProductName);

            Directory.CreateDirectory(path);

            return Path.Combine(path, file);
        }

        /// <summary>
        /// Checks whether access <paramref name="rights"/> are granted for 
        /// provided <paramref name="folder"/> path.
        /// </summary>
        /// <param name="folder">
        /// The folder path for which the access rights are to be determined.
        /// </param>
        /// <param name="rights">
        /// The bit-set of access rights to be evaluated.
        /// </param>
        /// <returns>
        /// True, if requested access rights are granted and false otherwise.
        /// </returns>
        private static Boolean IsAccessPermitted(String folder, FileSystemRights rights)
        {
            if (String.IsNullOrEmpty(folder))
            {
                return false;
            }

            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();

                AuthorizationRuleCollection rules = Directory
                    .GetAccessControl(folder)
                    .GetAccessRules(true, true, typeof(SecurityIdentifier));

                foreach (FileSystemAccessRule rule in rules)
                {
                    if (identity.Groups.Contains(rule.IdentityReference))
                    {
                        if (rule.AccessControlType == AccessControlType.Allow && rule.FileSystemRights.HasFlag(rights))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        #endregion
    }
}
