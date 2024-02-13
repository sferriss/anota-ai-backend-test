using Catalog.Application.Commands.Aws.Sns;
using Catalog.Domain.Notifications;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public partial class NotificationMapper
{
    public partial Notification ToNotification(SnsMessageCommand request);
}