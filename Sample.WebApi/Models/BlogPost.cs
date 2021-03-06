﻿    using System;
    using System.Collections.Generic;
    

namespace Sample.WebApi.Models
{

    /// <summary>
    /// Status Enum Summary
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Published Value summary
        /// </summary>
        Published,
        Retired,
        Draft
    }

    /// <summary>
    /// Blog Post Class Summary
    /// </summary>
    /// <remarks>
    /// Blog Post Class Remarks
    /// </remarks>
    public class BlogPost
    {
        public BlogPost()
        {
            SomeNumbers=new int[0];
            Data= new List<byte>().ToArray();
        }
        /// <summary>
        /// BlogPost.Title Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.Title Remarks
        /// </remarks>
        public string Title { get; set; }


        /// <summary>
        /// BlogPost.Status Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.Status Remarks
        /// </remarks>
        public Status Status { get; set; }



        /// <summary>
        /// BlogPost.InfoNums  Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.InfoNums  Remarks
        /// </remarks>
        public int? InfoNums { get; set; }
                  
        /// <summary>
        /// BlogPost.Data Data 
        /// </summary>
        /// <remarks>
        /// BlogPost.Data Data 
        /// </remarks>
        public Byte[] Data { get; set; }

        /// <summary>
        /// BlogPost.Id Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.Id Remarks
        /// </remarks>
        public int Id { get; set; }
        
        /// <summary>
        /// BlogPost.SomeNumbers Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.SomeNumbers Remarks
        /// </remarks>
        public int[] SomeNumbers { get; set; }

        /// <summary>
        /// BlogPost.PublishDate Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.PublishDate Remarks
        /// </remarks>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// BlogPost.Author (Person) Summary
        /// </summary>
        /// <remarks>
        /// BlogPost.Author (Person) Remarks
        /// </remarks>
        public Person Author { get; set; }

 
    }
}