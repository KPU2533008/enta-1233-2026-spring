using UnityEngine;

public class Sequence {

	public int Current { get; private set; } = 0;
	public int Size { get; private set; } = 0;
	public int RepeatCount { get; private set; } = -1;
	public bool Reverses { get; private set; } = false;
	public bool IsReversing { get; private set; } = false;

	public int Repetitions { get; private set; } = 0;

	public bool HasReachedEnd => Current == ( IsReversing ? 0 : Size - 1 );
	public bool IsFinished => Repetitions >= RepeatCount && RepeatCount >= 0;

	public Sequence(int size, int repeatCount, bool reverses) {
		Size = size;
		RepeatCount = repeatCount;
		Reverses = reverses;
	}

	public void AdvanceToNext() {
		bool isFinished = IsFinished;
		bool hasReachedEnd = HasReachedEnd;

		bool shouldReverse = Reverses && hasReachedEnd && !isFinished;
		bool shouldRepeat = ( Reverses ? ( IsReversing && hasReachedEnd ) : hasReachedEnd ) && !isFinished;

		if ( IsReversing && !Reverses )
			IsReversing = false;

		if ( shouldReverse )
			IsReversing = !IsReversing;

		if ( shouldRepeat )
			Repetitions++;

		int inc = IsReversing ? -1 : 1;
		Current = Mathf.Clamp(( Current + inc ) % Size, 0, Size - 1);
	}

}
