using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace gghud.Models.TimerBars
{
    /// <summary>
    /// Represents a timer bar which contains a visual representation of checkpoints.
    /// </summary>
    /// <remarks>
    /// A <see cref="CheckpointsTimerBar"/> can contain up to <see cref="MaxCheckpoints"/> checkpoints.
    /// <para>
    /// Each checkpoint is drawn as a circle which is either grayed out when not completed or of a specific color when completed.
    /// Additionally, it can have a colored cross drawn on top.
    /// </para>
    /// <para>
    /// The state of a checkpoint is defined using the <see cref="Checkpoint"/> structure and the <see cref="Checkpoints"/> property
    /// returns an array containing the <see cref="Checkpoint"/>s of a <see cref="CheckpointsTimerBar"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="LabeledTimerBar"/>
    /// <seealso cref="Checkpoint"/>
    public class CheckpointsTimerBar : LabeledTimerBar
    {
        /// <summary>
        /// Represents a checkpoint of a <see cref="CheckpointsTimerBar"/>.
        /// </summary>
        public struct Checkpoint
        {
            /// <summary>
            /// Gets or sets whether the <see cref="Checkpoint"/> is drawn as completed.
            /// </summary>
            /// <value>
            /// <see langword="true"/> if the <see cref="Checkpoint"/> is drawn as completed, otherwise <see langword="false"/>.
            /// </value>
            /// <remarks>
            /// When the <see cref="Checkpoint"/> is completed, it's drawn as a circle of the <see cref="Color"/> specified by <see cref="CompletedColor"/>,
            /// otherwise, it's drawn as a circle grayed out.
            /// </remarks>
            public bool IsCompleted { get; set; }
            /// <summary>
            /// Gets or sets the <see cref="Color"/> used when the <see cref="Checkpoint"/> is drawn and <see cref="IsCompleted"/> is <see langword="true"/>.
            /// The alpha component is ignored.
            /// </summary>
            /// <value>
            /// A <see cref="Color"/> value representing the completed color.
            /// </value>
            public Color CompletedColor { get; set; }
            /// <summary>
            /// Gets or sets whether a cross is drawn over the <see cref="Checkpoint"/>.
            /// </summary>
            /// <value>
            /// <see langword="true"/> if a cross is drawn over the <see cref="Checkpoint"/>, otherwise <see langword="false"/>.
            /// </value>
            public bool HasCross { get; set; }
            /// <summary>
            /// Gets or sets the <see cref="Color"/> of the cross drawn when the <see cref="Checkpoint"/> is drawn and <see cref="HasCross"/> is <see langword="true"/>.
            /// The alpha component is ignored.
            /// </summary>
            /// <value>
            /// A <see cref="Color"/> value representing the cross color.
            /// </value>
            public Color CrossColor { get; set; }
        }

        private static readonly TextureDictionary CheckpointTextureDictionary = new TextureDictionary("timerbars");
        private const string CheckpointTextureName = "circle_checkpoints";
        private static readonly TextureDictionary CrossTextureDictionary = new TextureDictionary("cross");
        private const string CrossTextureName = "circle_checkpoints_cross";
        // Constants from the game scripts
        private const float CheckpointXOffset = 0.1495f; // == ((((((((0.919f - 0.081f) + 0.004f) - 0.006f) + 0.05f) - 0.001f) - 0.005f) + 0.065f) - 0.0005f) - TimerBarManager.InitialX
        private const float CheckpointYOffset = ((((0.013f - 0.002f) + 0.001f) + 0.001f) - 0.001f);
        private const float CheckpointWidth = 0.012f;
        private const float CheckpointHeight = 0.023f;
        private const float CheckpointPadding = 0.0094f;
        /// <summary>
        /// Defines the maximum number of checkpoints for a <see cref="CheckpointsTimerBar"/>.
        /// </summary>
        public const int MaxCheckpoints = 8;

        private int visibleCheckpoints = 0;

        /// <summary>
        /// Gets the array of <see cref="Checkpoint"/>s. Its length is always equal to <see cref="MaxCheckpoints"/>
        /// </summary>
        /// <value>
        /// The array of <see cref="Checkpoint"/>s.
        /// </value>
        /// <remarks>
        /// The first item of the array corresponds to the right-most checkpoint and
        /// the following items correspond to the next checkpoints to the left.
        /// </remarks>
        public Checkpoint[] Checkpoints { get; } = new Checkpoint[MaxCheckpoints];
        /// <summary>
        /// Gets or sets the number of checkpoints drawn. It must be 0 or positive and less than or equal to <see cref="MaxCheckpoints"/>.
        /// </summary>
        /// <value>The number of visible checkpoints.</value>
        public int VisibleCheckpoints
        {
            get => visibleCheckpoints;
            set
            {
                if (value != visibleCheckpoints)
                {
                    if (visibleCheckpoints == MaxCheckpoints) visibleCheckpoints = 0;
                    else
                        visibleCheckpoints = value;
                }
            }
        }

        /// <inheritdoc/>
        protected internal override bool SmallHeight => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointsTimerBar"/> class with the specified label and number of visible checkpoints.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="visibleCheckpoints">The number of visible checkpoints. It must be 0 or positive and less than or equal to <see cref="MaxCheckpoints"/>.</param>
        public CheckpointsTimerBar(string label, int visibleCheckpoints) : base(label)
        {
            VisibleCheckpoints = visibleCheckpoints;
        }

        public override async Task Draw(Vector2 position)
        {
            if (!IsVisible)
                return;

            await base.Draw(position);

            if (VisibleCheckpoints > 0)
            {
                if (!CheckpointTextureDictionary.IsLoaded) CheckpointTextureDictionary.Load();
                if (!CrossTextureDictionary.IsLoaded) CrossTextureDictionary.Load();

                position.X += CheckpointXOffset;
                position.Y += CheckpointYOffset;

                for (int i = 0; i < VisibleCheckpoints; i++)
                {
                    Checkpoint d = Checkpoints[i];
                    if (d.IsCompleted)
                    {
                        Color c = d.CompletedColor;
                        DrawSprite(CheckpointTextureDictionary.Name, CheckpointTextureName, position.X, position.Y, CheckpointWidth, CheckpointHeight, 0.0f, c.R, c.G, c.B, 250);
                    }
                    else
                    {
                        DrawSprite(CheckpointTextureDictionary.Name, CheckpointTextureName, position.X, position.Y, CheckpointWidth, CheckpointHeight, 0.0f, 255, 255, 255, 51);
                    }

                    if (d.HasCross)
                    {
                        Color c = d.CrossColor;
                        DrawSprite(CrossTextureDictionary.Name, CrossTextureName, position.X, position.Y, CheckpointWidth, CheckpointHeight, 0.0f, c.R, c.G, c.B, 250);
                    }

                    position.X -= CheckpointPadding;
                }
            }
        }
    }
}