using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SoulWarriors
{
    public struct Frame
    {
        public readonly Rectangle sourceRectangle;
        public readonly Vector2 origin;
        public readonly int frameTime;

        /// <summary>
        /// Creates a new frame with a source rectangle, frame time and default center origin
        /// </summary>
        /// <param name="sourceRectangle">Position of frame in texture</param>
        /// <param name="frameTime">Time between this and next frame in milliseconds</param>
        public Frame(Rectangle sourceRectangle, int frameTime)
        {
            this.sourceRectangle = sourceRectangle;
            this.origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2); 
            this.frameTime = frameTime;
        }

        /// <summary>
        /// Creates a new frame with a source rectangle, frame time and default center origin
        /// </summary>
        /// <param name="sourceRectangle">Position of frame in texture</param>
        /// <param name="origin">origin for this frame</param>
        /// <param name="frameTime">Time between this and next frame in milliseconds</param>
        public Frame(Rectangle sourceRectangle, Vector2 origin, int frameTime)
        {
            this.sourceRectangle = sourceRectangle;
            this.origin = origin;
            this.frameTime = frameTime;
        }
    }

    public enum AnimationDirections
    {
        Up, Down, Left, Right
    }

    public enum AnimationsStates
    {
        Idle, Walk, Action1, Action2, Action3, Action4
    }

    // AnimationTypes consist of animationState + AnimationDirection
    public enum AnimationTypes
    {
        None,
        IdleUp,
        IdleDown,
        IdleLeft,
        IdleRight,
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight,
        Action1Up,
        Action1Down,
        Action1Left,
        Action1Right,
        Action2Up,
        Action2Down,
        Action2Left,
        Action2Right,
        Action3Up,
        Action3Down,
        Action3Left,
        Action3Right,
        Action4Up,
        Action4Down,
        Action4Left,
        Action4Right,
    }

    public class Animation
    {
        public readonly List<Frame> frames;
        private int _timeForCurrentFrame;

        public AnimationTypes AnimationType { get; }

        public int CurrentFrame { get; private set; }

        /// <summary>
        /// Total time for a single loop in milliseconds
        /// </summary>
        public int TotalFrameTime
        {
            get
            {
                int totalFrameTime = 0;

                for (int frame = 0; frame < frames.Count; frame++)
                {
                    totalFrameTime += frames[frame].frameTime;
                }

                return totalFrameTime;
            }
        }


        /// <summary>
        /// Initializes a new animation with an animation type and list of frames
        /// </summary>
        /// <param name="animationType">Animation type, used to identify the animation</param>
        /// <param name="frames"></param>
        public Animation(AnimationTypes animationType, List<Frame> frames)
        {
            AnimationType = animationType;
            this.frames = frames;
        }
        /// <summary>
        /// Initializes a new animation with the None animation type and list of frames
        /// </summary>
        /// <param name="frames"></param>
        public Animation(List<Frame> frames)
        {
            this.frames = frames;
            AnimationType = AnimationTypes.None;
        }

        /// <summary>
        /// Animates through the list of frames
        /// </summary>
        /// <param name="sourceRectangle">source rectangle to apply animation to</param>
        /// <param name="gameTime"></param>
        public void Animate(ref Rectangle sourceRectangle, GameTime gameTime)
        {
            // Update time elapsed for this frame
            _timeForCurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
            // If time has passed longer for this frame than this frame´s frameTime
            if (_timeForCurrentFrame >= frames[CurrentFrame].frameTime)
            {
                // Go to next frame in frames
                CurrentFrame = (CurrentFrame + 1) % frames.Count;
                // Set sourceRectangle to this frame
                sourceRectangle = frames[CurrentFrame].sourceRectangle;
                // Reset time elapsed
                _timeForCurrentFrame = 0;
            }
        }

        /// <summary>
        /// Animates through the list of frames
        /// </summary>
        /// <param name="sourceRectangle">source rectangle to apply animation to</param>
        /// <param name="origin">origin to apply animation to</param>
        /// <param name="gameTime"></param>
        public void Animate(ref Rectangle sourceRectangle, ref Vector2 origin, GameTime gameTime)
        {
            // Update time elapsed for this frame
            _timeForCurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
            // If time has passed longer for this frame than this frame´s frameTime
            if (_timeForCurrentFrame >= frames[CurrentFrame].frameTime)
            {
                // Go to next frame in frames
                CurrentFrame = (CurrentFrame + 1) % frames.Count;
                // Set object´s source rectangle to this frame´s source rectangle
                sourceRectangle = frames[CurrentFrame].sourceRectangle;
                // Set object´s origin this frame´s origin
                origin = frames[CurrentFrame].origin;
                // Reset time elapsed
                _timeForCurrentFrame = 0;
            }
        }

        /// <summary>
        /// Sets animation to the specified frame
        /// </summary>
        /// <param name="sourceRectangle"></param>
        /// <param name="frameToSet"></param>
        public void SetToFrame(ref Rectangle sourceRectangle, int frameToSet)
        {
            // Set animation to first frame
            CurrentFrame = frameToSet % frames.Count;
            // Set sourceRectangle to the first frame
            sourceRectangle = frames[CurrentFrame].sourceRectangle;
            // Reset time elapsed
            _timeForCurrentFrame = 0;
        }

        /// <summary>
        /// Resets animation
        /// </summary>
        public void Reset()
        {
            // Set animation to  frame
            CurrentFrame = 0;
            // Reset time elapsed
            _timeForCurrentFrame = 0;

        }
    }
}    
