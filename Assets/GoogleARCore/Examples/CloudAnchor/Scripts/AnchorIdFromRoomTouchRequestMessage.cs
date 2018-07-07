namespace GoogleARCore.Examples.CloudAnchor
{
    using System;
    using UnityEngine.Networking;

    /// <summary>
    /// Anchor identifier from room request message.
    /// </summary>
    public class AnchorIdFromRoomTouchRequestMessage : MessageBase
    {
        public int touch;

        /// <summary>
        /// Serialize the message.
        /// </summary>
        /// <param name="writer">Writer to write the message to.</param>
        public override void Serialize(NetworkWriter writer)
        {
            base.Serialize(writer);
            writer.Write(touch);
        }

        /// <summary>
        /// Deserialize the message.
        /// </summary>
        /// <param name="reader">Reader to read the message from.</param>
        public override void Deserialize(NetworkReader reader)
        {
            base.Deserialize(reader);
            touch = reader.ReadInt16();
        }
    }
}