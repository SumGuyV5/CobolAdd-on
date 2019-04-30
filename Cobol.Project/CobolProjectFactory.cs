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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace RichardAllen.Cobol.Project
{
    [Guid(GuidList.guidCobolProjectFactoryString)]
    class CobolProjectFactory : ProjectFactory
    {
        private CobolProjectPackage package;

        public CobolProjectFactory(CobolProjectPackage package)
            : base(package)
        {
            this.package = package;
        }

        protected override ProjectNode CreateProject()
        {
            CobolProjectNode project = new CobolProjectNode(this.package);

            project.SetSite((IOleServiceProvider)((IServiceProvider)this.package).GetService(typeof(IOleServiceProvider)));
            return project;
        }
    }
}
