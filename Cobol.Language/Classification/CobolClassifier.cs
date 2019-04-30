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

namespace RichardAllen.Cobol.Language.Classification
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ITaggerProvider))]
    [ContentType(CobolClassifierProvider.ContentType)]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CobolClassifierProvider : ITaggerProvider
    {
        public const string ContentType = "OpenCobolIDE";

        [Export]
        [Name(CobolClassifierProvider.ContentType)]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition CobolContentType = null;

        [Export]
        [FileExtension(".cob")]
        [ContentType(CobolClassifierProvider.ContentType)]
        internal static FileExtensionToContentTypeDefinition CobFileType = null;

        [Export]
        [FileExtension(".cbl")]
        [ContentType(CobolClassifierProvider.ContentType)]
        internal static FileExtensionToContentTypeDefinition CblFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {

            ITagAggregator<CobolTokenTag> cobolTagAggregator =
                                            aggregatorFactory.CreateTagAggregator<CobolTokenTag>(buffer);

            return new CobolClassifier(buffer, cobolTagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }
    }

    internal sealed class CobolClassifier : ITagger<ClassificationTag>
    {
        ITextBuffer _buffer;
        ITagAggregator<CobolTokenTag> _aggregator;
        IDictionary<CobolTokenTypes, IClassificationType> _cobolTypes;

        internal CobolClassifier(ITextBuffer buffer,
                               ITagAggregator<CobolTokenTag> cobolTagAggregator,
                               IClassificationTypeRegistryService typeService)
        {
            _buffer = buffer;
            _aggregator = cobolTagAggregator;
            _cobolTypes = new Dictionary<CobolTokenTypes, IClassificationType>();
            _cobolTypes[CobolTokenTypes.keywords] = typeService.GetClassificationType("keywords");
            _cobolTypes[CobolTokenTypes.divisions] = typeService.GetClassificationType("divisions");
            _cobolTypes[CobolTokenTypes.comments] = typeService.GetClassificationType("comments");
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (var tagSpan in this._aggregator.GetTags(spans))
            {
                var tagSpans = tagSpan.Span.GetSpans(spans[0].Snapshot);
                yield return
                    new TagSpan<ClassificationTag>(tagSpans[0],
                                                   new ClassificationTag(_cobolTypes[tagSpan.Tag.type]));
            }
        }
    }
}
