#region "  � Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

#if ! MINI
[assembly : AssemblyTitle("FileHelpers Lib   http://www.filehelpers.com")]
[assembly : AssemblyDescription("An easy to use file library for .NET that supports automatic formated file read/write operations.")]
[assembly : AssemblyProduct("FileHelpers   http://www.filehelpers.com")]
[assembly : ReflectionPermission(SecurityAction.RequestMinimum, ReflectionEmit = true)]
[assembly : SecurityPermission(SecurityAction.RequestMinimum, SerializationFormatter = true)]
#else
[assembly : AssemblyTitle("FileHelpers Lib (Pocket PC)  http://www.filehelpers.com")]
[assembly : AssemblyDescription("A simple to use file library for .NET Compact Framework that supports automatic formated file read/write operations.")]
[assembly : AssemblyProduct("FileHelpersPPC   http://www.filehelpers.com")]
#endif

[assembly : AssemblyVersion("2.2.0.0")]
[assembly : AssemblyCompany("Marcos Meli")]
[assembly : AssemblyCopyright("Copyright 2005-07. Marcos Meli")]
[assembly : AssemblyTrademark("FileHelpers")]
[assembly : AssemblyCulture("")]
[assembly : AssemblyConfiguration("")]
[assembly : ComVisible(false)]

[assembly : AssemblyDelaySign(false)]
[assembly : AssemblyKeyName("")]

#if NET_2_0
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FileHelpers.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010071634CEBD8DEBAE05841DF6D97B134B335B019F771467C700C64DE1A31EBE92784AA4EEE76C8E23D495622FFE910727BC2F24C41B7E46C61B88BF659B25034D58F685E533BC45F5CC26FB07AAAE85E86A931E97016DEA5D9D920E1C623433A45828BDAA5216F5FDE854673F26B6DEFAF7AA55706301CC94AF9B03BA3943288C5")]
#endif

#if ! NET_2_0
#if ! MINI
[assembly: AssemblyKeyFile(@"..\..\FileHelpers.snk")]
#endif
#endif