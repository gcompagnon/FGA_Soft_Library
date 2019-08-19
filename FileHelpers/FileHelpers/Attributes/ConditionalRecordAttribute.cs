#region "  � Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{

    /// <summary>Allow to declaratively set what records must be included or excluded when reading.</summary>
	/// <remarks>See the <a href="attributes.html">Complete attributes list</a> for more information and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes list</seealso>
	/// <seealso href="quick_start.html">Quick start guide</seealso>
	/// <seealso href="examples.html">Examples of use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConditionalRecordAttribute : Attribute
	{
		internal RecordCondition mCondition;
		internal string mConditionSelector;

        /// <summary>Allow to declaratively show what records must be included or excluded</summary>
		/// <param name="condition">The condition used to include or exclude each record</param>
		/// <param name="selector">The selector for the condition.</param>
		public ConditionalRecordAttribute(RecordCondition condition, string selector)
		{
		    ExHelper.CheckNullOrEmpty(selector, "selector");

			mCondition = condition;
			mConditionSelector = selector;
		}

	}

}
