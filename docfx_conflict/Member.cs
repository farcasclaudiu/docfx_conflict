using System;
using docfx_conflict.member;

namespace docfx_conflict
{
    /// <summary>
    /// This is a member class
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Member full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Member address.
        /// </summary>
        public MemberAddress Address { get; set; }
    }
}
