using System.ComponentModel.DataAnnotations;

namespace Shared;

public class RequestMessageDTO<TDataType>
{
    [Required(ErrorMessage = "EventName is required")]
    public string EventName {get; set;}
    public TDataType Data {get; set;}

}