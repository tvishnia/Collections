
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ComparerBasic.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class PictureController : ControllerBase
{
    private readonly IMediator _mediator;
    
    readonly static string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                                       Environment.OSVersion.Platform == PlatformID.MacOSX)
        ? Environment.GetEnvironmentVariable("HOME")
        : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

    readonly string guacHomePath = Path.Combine(homePath, "guac");
    public PictureController(IMediator mediator)
    {
        _mediator = mediator;
        // Get home directory based on if os is Windows or Unix - add guac directory Ex: "/home/user/guac"
        
    }
    // get list, create, update, delete

    // GET api/file/downlaod
    /// <summary>
    /// Return a locally stored file based on id to the requesting client
    /// </summary>
    /// <param name="id">unique identifier for the requested file</param>
    /// <returns>an IAction Result</returns>
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(string id)
    {
        string path = Path.Combine(guacHomePath, id);

        if (System.IO.File.Exists(path))
        {
            // Get all bytes of the file and return the file with the specified file contents 
            byte[] b = await System.IO.File.ReadAllBytesAsync(path);
            return File(b, "application/octet-stream");
        }

        else
        {
            // return error if file not found
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
    
    // GET api/file/upload
    /// <summary>
    /// Receive form data containing a file, save file locally with a unique id as the name, and return the unique id
    /// </summary>
    /// <param name="file">Received IFormFile file</param>
    /// <returns>IAction Result</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        // create the new file name consisting of the current time plus a GUID
        string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString();

        // Verify the home-guac directory exists, and combine the home-guac directory with the new file name
        Directory.CreateDirectory(guacHomePath);
        var filePath = Path.Combine(guacHomePath, newFileName);

        // Create a new file in the home-guac directory with the newly generated file name
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            //copy the contents of the received file to the newly created local file 
            await file.CopyToAsync(stream);
        }
        // return the file name for the locally stored file
        return Ok(newFileName);
    }
    
    
    
    // /// <summary>
    // /// Get all departments list
    // /// </summary>
    // /// <returns>PictureFileInfo List</returns>
    // /// <response code="200">Success</response>
    // [HttpGet] 
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // public async Task<ActionResult<IQueryable>> GetAll()
    // { 
    //         var query = new GetDepartmentListQuery();
    //
    //         IQueryable departments = await _mediator.Send(query);
    //         return Ok(departments); 
    //     }
    
    //
    // /// <summary>
    // /// Update ElectricalHazardousWorkPermit MainEvents and Ppe
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPut("UpdateMainEventsAndPpe")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>>? UpdateElectricalHazardousWorkPermitMainEventsAndPpe(UpdateElectricalHazardousWorkPermitMainEventsAndPpeDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitMainEventsAndPpeCommand
    //     (
    //         Id: request.Id,
    //         IsNightWork: request.IsNightWork,
    //         NatureOfWorkName: request.NatureOfWorkName,
    //         Note: request.Note
    //     );
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit team members
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddTeamMember")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitTeamMember(AddElectricalHazardousWorkPermitTeamMemberExtDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitTeamMemberCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         MemberId: request.MemberId,
    //         WorkerId: request.WorkerId,
    //         StartWorkDate: request.StartWorkDate,
    //         EndWorkDate: request.EndWorkDate,
    //         Comment: request.Comment);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit objects left under voltage
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddLeftUnderVoltage")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitLeftUnderVoltage(AddElectricalHazardousWorkPermitLeftUnderVoltageDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitLeftUnderVoltageCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         LeftUnderVoltageObjectId: request.ObjectUnderVoltageId,
    //         Note: request.Note);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit additional directions
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("AdditionalDirections")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitAdditionalDirections(AddElectricalHazardousWorkPermitAdditionalDirectionsDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitAdditionalDirectionsCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         AdditionalDirections: request.AdditionalDirections);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit under voltage note
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("UnderVoltageNote")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> UpdateElectricalHazardousWorkPermitLeftUnderVoltageNote(UpdateElectricalHazardousWorkPermitLeftUnderVoltageNoteDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitLeftUnderVoltageNoteCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         LeftUnderVoltageId: request.LeftUnderVoltageId,
    //         Note: request.Note);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Remove ElectricalHazardousWorkPermit team members
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpDelete("RemoveTeamMember")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitTeamMember(Guid request)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitTeamMemberCommand(TeamMemberId: request);
    //     await _mediator.Send(command);
    //     return request;
    // }
    //
    // /// <summary>
    // /// Remove ElectricalHazardousWorkPermit objects left under voltage
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpDelete("RemoveLeftUnderVoltage")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitLeftUnderVoltage(Guid request)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitLeftUnderVoltageCommand(LeftUnderVoltageId: request);
    //     await _mediator.Send(command);
    //     return request;
    // }
    //
    // /// <summary>
    // /// Close ElectricalHazardousWorkPermit
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("Close")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> CloseElectricalHazardousWorkPermit(Guid workPermitId)
    // {
    //     var command = new CloseElectricalHazardousWorkPermitCommand(WorkPermitId: workPermitId);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Cancel ElectricalHazardousWorkPermit
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("Cancel")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> CancelElectricalHazardousWorkPermit(Guid workPermitId)
    // {
    //     var command = new CancelElectricalHazardousWorkPermitCommand(WorkPermitId: workPermitId);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Issue ElectricalHazardousWorkPermit
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("Issue")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> IssueElectricalHazardousWorkPermit(Guid workPermitId)
    // {
    //     var command = new IssueElectricalHazardousWorkPermitCommand(WorkPermitId: workPermitId);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit extensions
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddExtension")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitExtension(AddElectricalHazardousWorkPermitExtentionDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitExtentionCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         EndWorkDate: request.EndWorkDate,
    //         Comment: request.Comment);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit responsible
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("SetResponsible")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> SetElectricalHazardousWorkPermitResponsible(SetElectricalHazardousWorkPermitResponsibleExtDto request)
    // {
    //     var command = new SetElectricalHazardousWorkPermitResponsibleCommand(
    //         WorkPermitId: request.WorkPermitId,
    //         ElectricResponsibleType: request.ElectricResponsibleType,
    //         ResponsibleId: request.ResponsibleId,
    //         WorkerId: request.WorkerId,
    //         NotNeeded: request.NotNeeded,
    //         Comment: request.Comment ?? string.Empty);
    //     ElectricalHazardousWorkPermitDto dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit additional events
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddAdditionalEvents")]
    // public async Task<ActionResult<StringDto>> AddElectricalHazardousWorkPermitAdditionalEvents(AddElectricalHazardousWorkPermitAdditionalEventsDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitAdditionalEventsCommand
    //     (
    //         Id: request.WorkPermitId,
    //         EventName: request.EventName,
    //         IsNightWork: request.IsNightWork,
    //         Note: request.Note
    //     );
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit additional Ppe
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddAdditionalPpe")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitAdditionalPpe(AddElectricalHazardousWorkPermitAdditionalPpeDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitAdditionalPpeCommand
    //     (
    //         Id: request.WorkPermitId,
    //         AdditionalPpeId: request.PpeId,
    //         Note: request.Note
    //     );
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Update Ppe note in ElectricalHazardousWorkPermit 
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPatch("UpdatePpeNote")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> UpdateElectricalHazardousWorkPermitPpeNote(UpdateElectricalHazardousWorkPermitPpeNoteDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitPpeNoteCommand
    //     (
    //         PpeId: request.PpeId,
    //         Note: request.Note
    //     );
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Update Event in ElectricalHazardousWorkPermit 
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPut("UpdateElectricalHazardousWorkPermitEvent")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> UpdateElectricalHazardousWorkPermitEvent(UpdateElectricalHazardousWorkPermitEventDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitEventCommand()
    //     {
    //         Id = request.Id,
    //         EventDescription = request.EventDescription,
    //         IsNightWork = request.IsNightWork,
    //         Note = request.Note
    //     };
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Remove Ppe from WorkPermit
    // /// </summary>
    // /// <returns>Guid</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpDelete("RemovePpe/{id}")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitAdditionalPpe(Guid id)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitAdditionalPpeCommand(id);
    //     var removedId = await _mediator.Send(command);
    //     return removedId;
    // }
    //
    // /// <summary>
    // /// Remove ElectricalHazardousWorkPermit additional events
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpDelete("RemoveAdditionalEvents")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitAdditionalEvent(Guid request)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitAdditionalEventsCommand(EventId: request);
    //     await _mediator.Send(command);
    //     return request;
    // }
    //
    // /// <summary>
    // /// Add ElectricalHazardousWorkPermit approver
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpPost("AddApprover")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitApprover(AddElectricalHazardousWorkPermitApproverExtDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitApproverCommand
    //     (
    //         WorkPermitId: request.WorkPermitId,
    //         ApproverId: request.ApproverId,
    //         WorkerId: request.WorkerId,
    //         IsOrganisationHead: request.IsOrganisationHead
    //     );
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Remove ElectricalHazardousWorkPermit approver
    // /// </summary>
    // /// <returns>ElectricalHazardousWorkPermitVm</returns>
    // /// <response code="200">Success</response>
    // /// <response code="401">If the user is unauthorized</response>
    // [HttpDelete("RemoveApprover/{approverId}")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitApprover(Guid approverId)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitApproverCommand(approverId);
    //     var id = await _mediator.Send(command);
    //     return Ok(id);
    // }
    //
    // /// <summary>
    // /// Добавление приложения
    // /// </summary>
    // /// <param name="request"></param>
    // /// <returns></returns>
    // [HttpPost("AddApplication")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> AddElectricalHazardousWorkPermitApplication(AddElectricalHazardousWorkPermitApplicationDto request)
    // {
    //     var command = new AddElectricalHazardousWorkPermitApplicationCommand(request.WorkPermitId, request.FileName, request.ApplicationFiles);
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Редактирование приложения
    // /// </summary>
    // /// <param name="request"></param>
    // /// <returns></returns>
    // [HttpPut("UpdateApplication")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> UpdateElectricalHazardousWorkPermitApplication(UpdateElectricalHazardousWorkPermitApplicatioDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitApplicationCommand(request.Id, request.FileName, request.ApplicationFiles);
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return Ok(vm);
    // }
    //
    // /// <summary>
    // /// Удаление приложения
    // /// </summary>
    // /// <param name="request"></param>
    // /// <returns></returns>
    // [HttpDelete("RemoveApplication/{id}")]
    // public async Task<ActionResult<Guid>> RemoveElectricalHazardousWorkPermitApplication(Guid id)
    // {
    //     var command = new RemoveElectricalHazardousWorkPermitApplicationCommand(ApplicationId: id);
    //     var removedId = await _mediator.Send(command);
    //     return removedId;
    // }
    //
    // /// <summary>
    // /// Изменить статус согласования НД ЭУ
    // /// </summary>
    // /// <param name="request"></param>
    // /// <returns></returns>
    // [HttpPut("UpdateWorkflowStatus")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> UpdateWorkflowStatus(UpdateElectricalHazardousWorkPermitWorkflowStatusDto request)
    // {
    //     var command = new UpdateElectricalHazardousWorkPermitWorkflowStatusCommand(
    //         request.WorkPermitId,
    //         request.FileName,
    //         request.StepDescription ?? string.Empty);
    //     var dto = await _mediator.Send(command);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //     return vm;
    // }
    //
    // /// <summary>
    // /// Изменить статус согласования НД ЭУ
    // /// </summary>
    // /// <param name="request"></param>
    // /// <returns></returns>
    // [HttpPut("ReOpenApprovalList")]
    // public async Task<ActionResult<ElectricalHazardousWorkPermitVm>> ReOpenApprovalList(Guid workPermitId)
    // {
    //     var command = new CreateElectricalHazardousWorkApprovalListCommand { WorkPermitId = workPermitId };
    //     await _mediator.Send(command);
    //
    //     var query = new ElectricalHazardousRealWorkPermitQuery(workPermitId);
    //     var dto = await _mediator.Send(query);
    //     var vm = _electricalHazardousWorkPermitService.DtoToVm(dto);
    //
    //     return vm;
    // }
}