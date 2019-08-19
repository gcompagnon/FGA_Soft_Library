#region "  � Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Diagnostics;
using System.ComponentModel;

namespace FileHelpers
{
	/// <summary>
	/// This class allows you to set some options of the records but at runtime.
	/// With this options the library is more flexible than never.
	/// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class RecordOptions
	{



#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        internal RecordInfo mRecordInfo;
	
		internal RecordOptions(RecordInfo info)
		{
			mRecordInfo = info;
            mRecordConditionInfo = new RecordConditionInfo(info);
			mIgnoreCommentInfo = new IgnoreCommentInfo(info);
		}

		/// <summary>The number of fields of the record type.</summary>
		public int FieldCount
		{
			get {return mRecordInfo.mFieldCount; }
		}

        // <summary>The number of fields of the record type.</summary>
        //[System.Runtime.CompilerServices.IndexerName("FieldNames")]
        //public string this[int index]
        //{
        //    get 
        //    {
        //        return mRecordInfo.mFields[index].mFieldInfo.Name; 
        //    }
        //}

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private string[] mFieldNames;

        /// <summary>Returns an string array with the fields names. (You mustn�t change the values of the array, clone it first if you need it)</summary>
        /// <returns>An string array with the fields names.</returns>
        public string[] FieldsNames
        {
			get
			{
				if (mFieldNames == null)
				{
					mFieldNames = new string[mRecordInfo.mFieldCount];
					for (int i = 0; i < mFieldNames.Length; i++)
						mFieldNames[i] = mRecordInfo.mFields[i].mFieldInfo.Name;
				}

				return mFieldNames;
			}
        }

        

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Type[] mFieldTypes;

        /// <summary>Returns a Type[] array with the fields types. (You mustn�t change the values of the array, clone it first if you need it)</summary>
        /// <returns>An Type[] array with the fields types.</returns>
        public Type[] FieldsTypes
        {
			get
			{
				if (mFieldTypes == null)
				{
					mFieldTypes = new Type[mRecordInfo.mFieldCount];
					for (int i = 0; i < mFieldTypes.Length; i++)
						mFieldTypes[i] = mRecordInfo.mFields[i].mFieldInfo.FieldType;
				}

				return mFieldTypes;
			}
        }

//        /// <summary>Returns the type of the field at the specified index</summary>
//        /// <returns>The type of the field.</returns>
//        /// <param name="index">The index of the field</param>
//        public Type GetFieldType(int index)
//        {
//            return mRecordInfo.mFields[index].mFieldInfo.FieldType;
//        }

        
		/// <summary>Indicates the number of first lines to be discarded.</summary>
		public int IgnoreFirstLines
		{
			get { return mRecordInfo.mIgnoreFirst; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreFirst= value;
			}
		}

		/// <summary>Indicates the number of lines at the end of file to be discarded.</summary>
		public int IgnoreLastLines
		{
			get { return mRecordInfo.mIgnoreLast; }
			set
			{
				ExHelper.PositiveValue(value);
				mRecordInfo.mIgnoreLast = value;
			}
		}

		/// <summary>Indicates that the engine must ignore the empty lines while reading.</summary>
		public bool IgnoreEmptyLines
		{
			get { return mRecordInfo.mIgnoreEmptyLines; }
			set { mRecordInfo.mIgnoreEmptyLines= value; }
		}

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private RecordConditionInfo mRecordConditionInfo;
		
		/// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        public RecordConditionInfo RecordCondition
        {
            get { return mRecordConditionInfo; }
        }


#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private IgnoreCommentInfo mIgnoreCommentInfo;

		/// <summary>Indicates that the engine must ignore the lines with this comment marker.</summary>
		public IgnoreCommentInfo IgnoreCommentedLines
		{
			get { return mIgnoreCommentInfo; }
		}
 
        /// <summary>Allow to tell the engine what records must be included or excluded while reading.</summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
		public sealed class RecordConditionInfo
        {
#if NET_2_0
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
            RecordInfo mRecordInfo;

            internal RecordConditionInfo(RecordInfo ri)
            {
                mRecordInfo = ri;
            }

            /// <summary>The condition used to include or exclude records.</summary>
            public RecordCondition Condition
            {
                get { return mRecordInfo.mRecordCondition; }
                set { mRecordInfo.mRecordCondition = value; }
            }

            /// <summary>The selector used by the <see cref="RecordCondition"/>.</summary>
            public string Selector
            {
                get { return mRecordInfo.mRecordConditionSelector; }
                set { mRecordInfo.mRecordConditionSelector = value; }
            }
        }



		/// <summary>Indicates that the engine must ignore the lines with this comment marker.</summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public sealed class IgnoreCommentInfo
		{
#if NET_2_0
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
            RecordInfo mRecordInfo;
			
            internal IgnoreCommentInfo(RecordInfo ri)
			{
				mRecordInfo = ri;
			}

			/// <summary>
			/// <para>Indicates that the engine must ignore the lines with this comment marker.</para>
			/// <para>An emty string or null indicates that the engine dont look for comments</para>
			/// </summary>
			public string CommentMarker
			{
				get { return mRecordInfo.mCommentMarker; }
				set
				{
					if (value != null)
						value = value.Trim();
					mRecordInfo.mCommentMarker = value;
				}
			}

			/// <summary>Indicates if the comment can have spaces or tabs at left (true by default)</summary>
			public bool InAnyPlace
			{
				get { return mRecordInfo.mCommentAnyPlace; }
				set { mRecordInfo.mCommentAnyPlace = value; }
			}
		}
    }

}
