using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._1233_StudentWork.Scripts.FSM {
    public abstract class FSM_State<TInput> {
        public FSM<TInput> FSM { get; internal set; } = null!;

        /// How long (in seconds) this state has been active.
        public float ElapsedTime { get; private set; }

        internal void ResetElapsedTime() => ElapsedTime = 0f;
        internal void AddElapsedTime(float deltaTime) => ElapsedTime += deltaTime;

        /// One-time setup after FSM is created and all states are registered
        public virtual void Init() { }

        public virtual void OnEnter(float deltaTime, in TInput input) { }
        public virtual void Step(float deltaTime, in TInput input) { }
        public virtual void OnLeave(float deltaTime, in TInput input) { }
    }

    /// <summary>
    /// Base transition: instance contains the Test() logic,
    /// while static metadata on the derived class declares From/To.
    /// </summary>
    public abstract class FSM_Transition<TInput> {
        // Set by FSM after construction so transitions can query state objects if needed.
        public FSM<TInput> FSM { get; internal set; } = null!;

        /// <summary>Return true to trigger this transition.</summary>
        public abstract bool Test(float deltaTime, in TInput input);
    }

    /// <summary>
    /// Derived transitions should override static metadata via "new static".
    /// C# doesn't support virtual statics on older language versions, so FSM reads these via reflection.
    /// </summary>
    public abstract class FSM_TransitionMeta<TInput> : FSM_Transition<TInput> {
        // "Static class" feel: declare these on the transition type.
        public static Type[] From => Array.Empty<Type>();
        public static Type To => typeof(FSM_State<TInput>); // placeholder; must be overridden with "new static"
    }

}
