//namespace AuditTestApi
//{
//    using Newtonsoft.Json;

//    using System;
//    using System.Collections.Generic;

//    public class ObjectId
//    {
//        [JsonProperty("$oid")]
//        public string Oid { get; set; }
//    }

//    public class PrimaryKey
//    {
//        [JsonProperty("Id")]
//        public int Id { get; set; }
//    }


//    public class Change
//    {
//        [JsonProperty("ColumnName")]
//        public string ColumnName { get; set; }

//        [JsonProperty("OriginalValue")]
//        public object OriginalValue { get; set; }

//        [JsonProperty("NewValue")]
//        public object NewValue { get; set; }
//    }


//    public class Entry
//    {
//        [JsonProperty("Table")]
//        public string Table { get; set; }

//        [JsonProperty("Name")]
//        public string Name { get; set; }

//        [JsonProperty("PrimaryKey")]
//        public PrimaryKey PrimaryKey { get; set; }

//        [JsonProperty("Action")]
//        public string Action { get; set; }

//        [JsonProperty("Entity")]
//        public Dictionary<string, object> Entity { get; set; }

//        [JsonProperty("Changes")]
//        public List<Change> Changes { get; set; }

//        [JsonProperty("ColumnValues")]
//        public Dictionary<string, object> ColumnValues { get; set; }

//        [JsonProperty("Valid")]
//        public bool Valid { get; set; }
//    }

//    public class EntityFrameworkEvent
//    {
//        [JsonProperty("ContextId")]
//        public string ContextId { get; set; }

//        [JsonProperty("Entries")]
//        public List<Entry> Entries { get; set; }

//        [JsonProperty("Result")]
//        public int Result { get; set; }

//        [JsonProperty("Success")]
//        public bool Success { get; set; }
//    }

//    public class Environment
//    {
//        [JsonProperty("UserName")]
//        public string UserName { get; set; }

//        [JsonProperty("MachineName")]
//        public string MachineName { get; set; }

//        [JsonProperty("DomainName")]
//        public string DomainName { get; set; }

//        [JsonProperty("CallingMethodName")]
//        public string CallingMethodName { get; set; }

//        [JsonProperty("AssemblyName")]
//        public string AssemblyName { get; set; }

//        [JsonProperty("Culture")]
//        public string Culture { get; set; }
//    }

//    public class AuditEvent
//    {
//        public object Id { get; set; }

//        [JsonProperty("EntityFrameworkEvent")]
//        public EntityFrameworkEvent EntityFrameworkEvent { get; set; }

//        [JsonProperty("EventType")]
//        public string EventType { get; set; }

//        [JsonProperty("Environment")]
//        public Environment Environment { get; set; }

//        [JsonProperty("StartDate")]
//        public DateTime StartDate { get; set; }

//        [JsonProperty("EndDate")]
//        public DateTime EndDate { get; set; }

//        [JsonProperty("Duration")]
//        public int Duration { get; set; }
//    }


//}
