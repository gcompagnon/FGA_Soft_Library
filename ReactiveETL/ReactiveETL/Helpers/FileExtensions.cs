﻿using System.IO;
using ReactiveETL.Operations.File;
using ReactiveETL.Activators;
using System;
using ReactiveETL.Files;

namespace ReactiveETL
{
    /// <summary>
    /// Extension Methods for files
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Write the result to a file
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="filename">full path to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, string filename)
        {
            return observed.WriteFile<T>(filename, null);
        }

        /// <summary>
        /// Write the result to a file
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="filename">full path to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, string filename, Action<FluentFile> prepare)
        {
            FileWriteActivator<T> activator = new FileWriteActivator<T>() { FileName = filename, PrepareFluentFile = prepare };
            FileWriteOperation<T> resoperation = new FileWriteOperation<T>(activator);
            observed.Subscribe(resoperation);
            return resoperation;
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="strm">stream to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, Stream strm)
        {
            return observed.WriteFile<T>(strm, null);
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="strm">stream to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, Stream strm, Action<FluentFile> prepare)
        {
            FileWriteActivator<T> activator = new FileWriteActivator<T>() { Stream = strm, PrepareFluentFile = prepare };
            FileWriteOperation<T> resoperation = new FileWriteOperation<T>(activator);
            observed.Subscribe(resoperation);
            return resoperation;
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="writer">stream to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, TextWriter writer)
        {
            return observed.WriteFile<T>(writer, null);
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <typeparam name="T">The poco type used to define file format</typeparam>
        /// <param name="observed">observed operation</param>
        /// <param name="writer">stream to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation<T> WriteFile<T>(this IObservableOperation observed, TextWriter writer, Action<FluentFile> prepare)
        {
            FileWriteActivator<T> activator = new FileWriteActivator<T>() { Writer = writer, PrepareFluentFile = prepare };
            FileWriteOperation<T> resoperation = new FileWriteOperation<T>(activator);
            observed.Subscribe(resoperation);
            return resoperation;
        }

        #region Version No Generics

        /// <summary>
        /// Write the result to a file
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="filename">full path to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, string filename,Type T)
        {
            return observed.WriteFile(filename, null,T);
        }

        /// <summary>
        /// Write the result to a file
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="filename">full path to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, string filename, Action<FluentFile> prepare, Type T)
        {
            FileWriteActivator activator = new FileWriteActivator() { FileName = filename, PrepareFluentFile = prepare, Type = T };
            FileWriteOperation resoperation = new FileWriteOperation(activator,T);
            observed.Subscribe(resoperation);
            return resoperation;
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="strm">stream to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, Stream strm, Type T)
        {
            return observed.WriteFile(strm, null,T);
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="strm">stream to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, Stream strm, Action<FluentFile> prepare, Type T)
        {
            FileWriteActivator activator = new FileWriteActivator() { Stream = strm, PrepareFluentFile = prepare, Type = T };
            FileWriteOperation resoperation = new FileWriteOperation(activator,T);
            observed.Subscribe(resoperation);
            return resoperation;
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="writer">stream to the file</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, TextWriter writer,Type T)
        {
            return observed.WriteFile(writer, null,T);
        }

        /// <summary>
        /// Write the result to a stream
        /// </summary>
        /// <param name="T">The poco type used to define file format</param>
        /// <param name="observed">observed operation</param>
        /// <param name="writer">stream to the file</param>
        /// <param name="prepare">Callback to prepare the file engine</param>
        /// <returns>file write operation</returns>
        public static FileWriteOperation WriteFile(this IObservableOperation observed, TextWriter writer, Action<FluentFile> prepare, Type T)
        {
            FileWriteActivator activator = new FileWriteActivator() { Writer = writer, PrepareFluentFile = prepare, Type = T };
            FileWriteOperation resoperation = new FileWriteOperation(activator,T);
            observed.Subscribe(resoperation);
            return resoperation;
        }
        #endregion
    }
}
