﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ReactiveETL.Files;

namespace ReactiveETL.Activators
{
    /// <summary>
    /// Activator for file writing
    /// </summary>
    public class FileWriteActivator
    {
        /// <summary>
        /// Text writer to the file
        /// </summary>
        public TextWriter Writer { get; set; }
        private TextWriter _innerstrmwriter;

        /// <summary>
        /// Name of the file to write
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Stream to the file to write
        /// </summary>
        public Stream Stream { get; set; }
        
        /// <summary>
        /// File engine in use
        /// </summary>
        public FileEngine Engine
        {
            get;
            private set;
        }

        /// <summary>
        /// Type  in use
        /// </summary>
        public Type Type
        {
            get;
            set;
        }


        /// <summary>
        /// Callback method to initialize the engine
        /// </summary>
        public Action<FluentFile> PrepareFluentFile { get; set; }

        /// <summary>
        /// Initialize the file engine
        /// </summary>
        /// <returns>initialized engine</returns>
        public FileEngine InitializeEngine()
        {
            if (Engine != null)
                return Engine;

            if (this.Stream != null && _innerstrmwriter == null)
            {
                _innerstrmwriter = new StreamWriter(this.Stream);
            }

            var ff = FluentFile.For(Type);
            if (PrepareFluentFile != null)
                PrepareFluentFile(ff);
            
            if (_innerstrmwriter != null)
            {
                Engine = ff.To(_innerstrmwriter);
            }
            else if (Writer != null)
            {
                Engine = ff.To(Writer);
            }
            else if (FileName != null)
            {
                Engine = ff.To(FileName);
            }

            if (Engine == null)
                throw new InvalidOperationException("File write is not initialized appropriately");            

            return Engine;
        }

        /// <summary>
        /// Release the file engine and all necessary ressources
        /// </summary>
        public void Release()
        {
            if (Engine != null)
                Engine.Dispose();
            
            if (_innerstrmwriter != null)
                _innerstrmwriter.Dispose();
        }
    }
}
