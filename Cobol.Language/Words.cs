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

namespace RichardAllen.Cobol.Language
{
    internal sealed class Words
    {
        public static readonly string[] keywordlst = { "accept", "add", "begin transaction", 
            "call", "close", "compute", "copy", "delete", "display", "divide", "evaluate",
            "exit", "go to", "if", "else", "inspect", "move", "multiply", "open", "perform",
            "read", "search", "set", "sort", "start", "subtract", "write", "pic", "display" };

        public static readonly string[] divisionlst = { "identification division", "environment division", "data division", "procedure division" };

        public static readonly string[] commentlst = { "*" };
    }
}
