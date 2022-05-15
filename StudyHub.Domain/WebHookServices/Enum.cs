using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.WebHookServices
{
    public enum HookResourceAction
    {
        undefined,
        hook_created,
        hook_removed,
        hook_updated,
        // etc...
    }

    // Actions of ProjectEventType
    public enum projectAction
    {
        undefined,
        project_created,
        project_renamed,
        project_archived,
        //etc...
    }


    public enum HookEventType
    {

        // Take this as an example, you can implement any event source you like.
        hook, //(Hook created, Hook deleted ...)

        file, // (Some file uploaded, file deleted)

        note, // (Note posted, note updated)

        project, // (Some project created, project dissabled)

        milestone // (Milestone created, milestone is done etc...)

        //etc etc.. You can define your custom events types....
    }

    public enum RecordResult
    {
        undefined = 0,
        ok,
        parameter_error,
        http_error,
        dataQueryError
    }
    public enum EventType
    {
        WebHook,
        System,
        Project,
    }
}
