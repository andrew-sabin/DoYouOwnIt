using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Enums
{
    /*
     * OwnershipLevel Enum:
     * -------
     * This enum represents the ownership level of a user regarding the format for a product.
     * Each value corresponds to a specific ownership status:
     * 0.) DontOwn - The user does not own the product, even when they have purchased it from the store.
     * 1.) Unknown - It is unclear if the user has any ownership over the format they are purchasing the product.
     * 2.) Licensed - The user kind of owns the product, but it is tied to a license that may be revoked at any time.
     * 3.) MissingFeatures - The user owns the product, but some features can be missing after a certain time due to online services being shut down.
     * 4.) NeedsMods - The user owns the product, but it requires additional modifications or patches to function correctly.
     * 5.) FullOwnership - The user fully owns the product, with no restrictions or missing features.
     */
    public enum OwnershipLevel
    {
        [Display(Name = "Don't Own", Description = "You do not own the product under the specific format or circumstance of purchase.", GroupName ="Not Likely")]
        DontOwn = 0,
        [Display(Name = "Unknown Ownership", Description = "Currently ambiguous if you own the product under the specific format or circumstance of purchase.", GroupName = "Not Likely")]
        Unknown = 1,
        [Display(Name = "Licensed Ownership", Description = "Ownership is under a license. Meaning the product can be removed from the owners library at any given time.", GroupName = "Not Likely")]
        Licensed = 2,
        [Display(Name = "Missing Features", Description = "You can own the product, but some features might be unavailable.", GroupName = "Missing Content")]
        MissingFeatures = 3,
        [Display(Name = "Needs Modifications For Full Functionality", Description = "You own the product. But you might need to do some modifications to fully own it.", GroupName = "Missing Content")]
        NeedsMods = 4,
        [Display(Name = "Full Ownership", Description= "You own the product.", GroupName ="Fully Owned")]
        FullOwnership = 5
    }
}
