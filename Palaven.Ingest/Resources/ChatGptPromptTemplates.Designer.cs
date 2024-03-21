﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Palaven.Ingest.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ChatGptPromptTemplates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ChatGptPromptTemplates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Palaven.Ingest.Resources.ChatGptPromptTemplates", typeof(ChatGptPromptTemplates).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are an AI assistant. You must create a list of questions in spanish from a text in spanish. Use the following directions to create the questions.
        ///1. The input text will be delimited by the tags &lt;working_text&gt;&lt;/working_text&gt;
        ///2. Create intelligent questions that can be answered reading the input text.
        ///3. You must generate a valid response in JSON with the following structure:
        ///{
        ///&apos;success&apos;: {true|false},
        ///&apos;questions&apos;:[]
        ///}
        ///4. If the text is too short to generate 15 questions, then generate 10 question [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateQuestionsFromArticlePromptTemplate {
            get {
                return ResourceManager.GetString("CreateQuestionsFromArticlePromptTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are an AI assistant. You must create an accurate short summary of a text in spanish.
        ///1. The input text will be delimited by the tags &lt;working_text&gt;&lt;/working_text&gt;
        ///2. Create an accurate short summary of the text.
        ///3. You must generate a valid response in JSON with the following structure:
        ///{
        ///&apos;success&apos;: {true|false},
        ///&apos;summary&apos;:{short_summary}
        ///}
        ///4. If you cannot create the requested summary, then assign false to the success response property and leave empty the summary property. Otherwise assign true [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateShortSummaryPromptTemplate {
            get {
                return ResourceManager.GetString("CreateShortSummaryPromptTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are an AI assistant. You must extract a text in a specific format with the following instructions:
        ///1. The text will be delimited by the tags &lt;working_text&gt;&lt;/working_text&gt;
        ///2. The text is in Spanish.
        ///3. You must generate a valid response in JSON with the following structure:
        ///{
        ///&apos;success&apos;: {true|false}
        ///&apos;article&apos;:{article_identifier},
        ///&apos;content&apos;:{article_content}
        ///&apos;references&apos;: {article_references}
        ///}
        ///4. Next I will tell you how to find the {article_identifier} and how to create the {article_content}  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ExtractArticlePromptTemplate {
            get {
                return ResourceManager.GetString("ExtractArticlePromptTemplate", resourceCulture);
            }
        }
    }
}
