using Catalog.Domain.Notifications.Enums;

namespace Catalog.Domain.Notifications;

public record Notification(string OwnerId, string ItemId, OperationType Type, ItemType ItemType);