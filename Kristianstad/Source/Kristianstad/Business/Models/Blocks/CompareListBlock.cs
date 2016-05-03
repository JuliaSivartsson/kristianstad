using System.ComponentModel.DataAnnotations;
using EPiCore.Content.Models.Misc;
using EPiCore.Models.Blocks;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;

namespace Kristianstad.Business.Models.Blocks
{
    [ContentType(
        GUID = "8320967C-A234-44D3-8AB0-6AC3C06DD3A2",
        DisplayName = "Compare list block",
        GroupName = GroupNames.Content)]
    public class CompareListBlock : BaseBlock
    {
        [Display(
            Name = "Header",
            Description = "Enter a header for the block",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        [Required]
        public virtual string Header { get; set; }
    }
}