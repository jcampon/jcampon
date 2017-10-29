using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCampon.MongoDB.Entities;
using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
	public class SampleEntity : MongoDbEntity
	{
		public string EntityName { get; set; }
	}
}
