#region "  � Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	internal sealed class EnumConverter : ConverterBase
	{
	    readonly Type mEnumType;

		public EnumConverter(Type sourceEnum)
		{
			if (sourceEnum.IsEnum == false)
				throw new BadUsageException("The sourceType must be an Enum and is of type " + sourceEnum.Name);

			mEnumType = sourceEnum;
		}

		public override object StringToField(string from)
		{
			try
			{
				return Enum.Parse(mEnumType, from.Trim(), true);
			}
			catch (ArgumentException)
			{
				throw new ConvertException(from, mEnumType, "The value don't is on the Enum.");
			}
		}
		
	}
}