﻿using MongoDB.Bson.Serialization.Attributes;

namespace AuditUI.Shared
{




	[BsonIgnoreExtraElements]

	public class EventAudit
	{

		public EventAudit()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public EntityFrameworkEvent EntityFrameworkEvent { get; set; }

		public DateTime StartDate { get; set; }

		/// <summary>
		/// The date then the event finished
		/// </summary>
		public DateTime? EndDate { get; set; }

		public bool ShowDetails { get; set; }
	}

	[BsonIgnoreExtraElements]
	public class EntityFrameworkEvent
	{
		//public string ContextId { get; set; }

		public List<EventEntry> Entries { get; set; }

		// public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();

		/// <summary>
		/// Returns the DbContext associated to this event
		/// </summary>

	}


	[BsonIgnoreExtraElements]
	public class EventEntry
	{
		public string Schema { get; set; }
		public string Table { get; set; }
		public string Name { get; set; }
		public IDictionary<string, object> PrimaryKey { get; set; }
		public string Action { get; set; }
		//public object Entity { get; set; }
		public List<EventEntryChange> Changes { get; set; }
		public IDictionary<string, object> ColumnValues { get; set; }
		//public bool Valid { get; set; }
		//public List<string> ValidationResults { get; set; }

		// public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();

	}

	[BsonIgnoreExtraElements]
	public class EventEntryChange
	{
		public string ColumnName { get; set; }
		public object OriginalValue { get; set; }
		public object NewValue { get; set; }
	}
}
