using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElasticSearch.Client.Mapping
{
	public class StringFieldSetting : AbstractFieldSetting
	{
		public StringFieldSetting()
		{
			Type = "string";
		}

		[JsonProperty("analyzer")]
		public string Analyzer;
		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer;
		[JsonProperty("omit_norms")]
		public bool OmitNorms;
		[JsonProperty("omit_term_freq_and_positions")]
		public bool OmitTermFreqAndPositions;
		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer;
		[JsonProperty("term_vector")]
		[JsonConverter(typeof(StringEnumConverter))]
		public TermVector TermVector;
	}

	public class NumberFieldSetting : AbstractFieldSetting
	{
		public NumberFieldSetting()
		{
			Type ="integer";
		}

        public NumberFieldSetting(NumType type)
        {
            Type = type.ToString().ToLower();
        }

		[JsonProperty("precision_step")]
		public int PrecisionStep = 4;
	}

	public class DateFieldSetting : AbstractFieldSetting
	{
		public DateFieldSetting()
		{
            Type = "date";
		}
		[JsonProperty("format")]
		public string Format;
		[JsonProperty("precision_step")]
		public int PrecisionStep=4;
	}

	public class BooleanFieldSetting : AbstractFieldSetting
	{
		public BooleanFieldSetting()
		{
			Type = "boolean";
		}
	}

	public class AbstractFieldSetting
	{
		[JsonIgnore] public string Name;

		[JsonProperty("type")]
		public string Type;
		[JsonProperty("boost")]
		public double Boost = 1.0;
		[JsonProperty("include_in_all")]
		public bool IncludeInAll = true;
		[JsonProperty("index")]
		[JsonConverter(typeof(StringEnumConverter))]
		public IndexType Index = IndexType.analyzed;
		[JsonProperty("index_name")]
		public string IndexName;
		[JsonProperty("null_value")]
		public object NullValue;
		[JsonProperty("store")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Store Store = Store.no;

	}
}