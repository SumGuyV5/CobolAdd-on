/************************************************************************
**  Program Name:   Cobol Add-on			      		                **
**  Version Number: V0.6                                     		    **
**  Copyright (C):  September 20, 2011  Richard W. Allen	   	        **
**  Date Started:   September 1, 2011	                      		    **
**  Date Ended:     September 20, 2011    	             		        **
**  Author:         Richard W. Allen                                    **
**  Webpage:        http://www.richardallenonline.com			        **
**  IDE:            Visual Studio 2010 SP1				                **
**  Compiler:	    C# 4.0				      		                    **
**  Langage:        C#							                        **
**  License:	    GNU GENERAL PUBLIC LICENSE Version 2		        **
**		            see license.txt for for details	      		        **
*************************************************************************/
// Guids.cs
// MUST match guids.h
using System;

namespace RichardAllen.Cobol.Project
{
    static class GuidList
    {
        public const string guidCobolProjectPkgString = "4902c23e-2c05-4237-a771-38044e03ab2b";
        public const string guidCobolProjectCmdSetString = "cdff8fd3-6aaf-4a16-9827-57b71ac18b44";
        public const string guidCobolProjectFactoryString = "621FD830-0A90-4378-91BC-98261D1173C7";

        public static readonly Guid guidCobolProjectCmdSet = new Guid(guidCobolProjectCmdSetString);
        public static readonly Guid guidCobolProjectFactory = new Guid(guidCobolProjectFactoryString);
    };
}