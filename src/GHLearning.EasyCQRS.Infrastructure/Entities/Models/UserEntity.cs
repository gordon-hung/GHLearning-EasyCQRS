using System.ComponentModel.DataAnnotations;
using GHLearning.EasyCQRS.Core.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GHLearning.EasyCQRS.Infrastructure.Entities.Models;
public record UserEntity
{
	/// <summary>
	/// Gets or sets the code.
	/// </summary>
	/// <value>
	/// The code.
	/// </value>
	[Key]
	[BsonId]
	[BsonRepresentation(BsonType.String)]
	public string Code { get; set; } = default!;
	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	/// <value>
	/// The username.
	/// </value>
	[BsonElement("username")]
	[BsonRepresentation(BsonType.String)]
	public string Username { get; set; } = default!;
	/// <summary>
	/// Gets or sets the password.
	/// </summary>
	/// <value>
	/// The password.
	/// </value>
	[BsonElement("password")]
	[BsonRepresentation(BsonType.String)]
	public string Password { get; set; } = default!;
	/// <summary>
	/// Gets or sets the status.
	/// </summary>
	/// <value>
	/// The status.
	/// </value>
	[BsonElement("status")]
	[BsonRepresentation(BsonType.String)]
	public UserStatus Status { get; set; }
	/// <summary>
	/// Gets or sets the created at.
	/// </summary>
	/// <value>
	/// The created at.
	/// </value>
	[BsonElement("created_at")]
	public DateTime CreatedAt { get; set; }
	/// <summary>
	/// Gets or sets the updated at.
	/// </summary>
	/// <value>
	/// The updated at.
	/// </value>
	[BsonElement("updated_at")]
	public DateTime UpdatedAt { get; set; }
	/// <summary>
	/// Gets or sets the registered at.
	/// </summary>
	/// <value>
	/// The registered at.
	/// </value>
	[BsonElement("registered_at")]
	public DateTime? RegisteredAt { get; set; }
	/// <summary>
	/// Gets or sets the deleted at.
	/// </summary>
	/// <value>
	/// The deleted at.
	/// </value>
	[BsonElement("deleted_at")]
	public DateTime? DeletedAt { get; set; }
}
