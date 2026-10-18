using Google.Cloud.Firestore;
using SameApi.Data.Model;

namespace SameApi.Model
{
    [FirestoreData]
    public class CommentDao : IModelDao
    {
        [FirestoreDocumentId] 
        public string Id { get; set; } = "";

        [FirestoreProperty("postId")]
        public string PostId { get; set; } = "";

        [FirestoreProperty("parentCommentId")] 
        public string? ParentCommentId { get; set; }

        [FirestoreProperty("authorId")] 
        public string AuthorId { get; set; } = "";

        [FirestoreProperty("text")] 
        public string Text { get; set; } = "";

        [FirestoreProperty("createdAt")] 
        public Timestamp CreatedAt { get; set; }

        [FirestoreProperty("isDeleted")] 
        public bool IsDeleted { get; set; }
    }
}