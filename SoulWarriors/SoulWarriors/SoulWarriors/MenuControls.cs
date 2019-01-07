using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    /// <summary>
    ///     An object used for easy control in menus using the keyboard as input
    /// </summary>
    class MenuControls
    {
        /// <summary>
        ///     Number of menu options
        /// </summary>
        private readonly Vector2 _selectionMax;


        /// <summary>
        ///     Creates a new instance of MenuControls
        /// </summary>
        /// <param name="selectionMax">Number of menu options</param>
        public MenuControls(Vector2 selectionMax)
        {
            _selectionMax = selectionMax;
        }

        /// <summary>
        ///     Updates selected menu option in menus
        /// </summary>
        /// <returns>vector2 passed by reference to change</returns>
        public void UpdateSelected(ref Vector2 selected, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            // When W or UP arrow keys are pressed
            if (currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.W) || currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.Up))
            {
                // And selected.Y is GREATER THAN 0, preventing it from exiting the selection range, 
                if (selected.Y > 0)
                {
                    // Then move selected
                    selected.Y--;
                }
            }

            // When A or Left arrow keys are pressed
            if (currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.A) || currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.Left))
            {
                // And selected.X is GREATER THAN 0, preventing it from exiting the selection range, 
                if (selected.X > 0)
                {
                    // Then move selected
                    selected.X--;
                }
            }

            // When S or Down arrow keys are pressed
            if (currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.S) || currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.Down))
            {
                // And selected.Y is LESS THAN selectionMax.Y, preventing it from exceeding maximum Y selection range, 
                if (selected.Y < _selectionMax.Y)
                {
                    // Then move selected
                    selected.Y++;
                }
            }
            // When D or Right arrow keys are pressed
            if (currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.D) || currentKeyboardState.SingleActivationKey(previousKeyboardState, Keys.Right))
            {
                // And selected.X is LESS THAN selectionMax.X, preventing it from exceeding maximum X selection range, 
                if (selected.X < _selectionMax.X)
                {
                    // Then move selected
                    selected.X++;
                }

            }
        }
    }
}