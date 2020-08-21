namespace Dreamteck.Forever
{
    using UnityEngine;
    using System.Collections.Generic;
    using UnityEngine.Serialization;
    using UnityEngine.SceneManagement;

    [System.Serializable]
    public class Level
    {
        public bool enabled = true;
        public string title = "";
        public SegmentSequenceCollection sequenceCollection = new SegmentSequenceCollection();
        public bool remoteSequence = false;
        public string remoteSceneName = "";
        public ThreadPriority loadingPriority = ThreadPriority.BelowNormal;
        public delegate void SequenceHandler(SegmentSequence sequence);
        public event SequenceHandler onSequenceEntered;
        private Stack<SegmentSequence> sequenceStack = new Stack<SegmentSequence>();
        private SegmentSequence lastSequence;

        private SegmentSequence[] sequences
        {
            get { return sequenceCollection.sequences; }
        }

        public bool isReady
        {
            get
            {
                return !remoteSequence || _isRemoteLoaded;
            }
        }

        private bool _isRemoteLoaded = false;

        public System.Collections.IEnumerator Load()
        {
            if (_isRemoteLoaded) yield break;
            Scene checkScene = SceneManager.GetSceneByName(remoteSceneName);
            if (!checkScene.isLoaded)
            {
                ThreadPriority lastPriority = Application.backgroundLoadingPriority;
                Application.backgroundLoadingPriority = loadingPriority;
                AsyncOperation async = SceneManager.LoadSceneAsync(remoteSceneName, LoadSceneMode.Additive);
                yield return async;
                Application.backgroundLoadingPriority = lastPriority;
            }
            RemoteLevel[] remoteLevels = Object.FindObjectsOfType<RemoteLevel>();
            Scene scene = SceneManager.GetSceneByName(remoteSceneName);
            for (int i = 0; i < remoteLevels.Length; i++)
            {
                if (remoteLevels[i].gameObject.scene.path == scene.path)
                {
                    sequenceCollection = remoteLevels[i].sequenceCollection;
                    break;
                }
            }
            _isRemoteLoaded = true;
        }

        public System.Collections.IEnumerator Unload()
        {
            if (!_isRemoteLoaded) yield break;
            Scene checkScene = SceneManager.GetSceneByName(remoteSceneName);
            if (checkScene.isLoaded)
            {
                ThreadPriority lastPriority = Application.backgroundLoadingPriority;
                Application.backgroundLoadingPriority = loadingPriority;
                AsyncOperation async = SceneManager.UnloadSceneAsync(remoteSceneName);
                yield return async;
                Application.backgroundLoadingPriority = lastPriority;
            }
            _isRemoteLoaded = false;
        }

        public void UnloadImmediate()
        {
            if (!_isRemoteLoaded) return;
            Scene checkScene = SceneManager.GetSceneByName(remoteSceneName);
            if (checkScene.isLoaded) SceneManager.UnloadSceneAsync(remoteSceneName);
            _isRemoteLoaded = false;
        }

        public void Initialize()
        {
            lastSequence = null;
            sequenceStack.Clear();
            for (int i = 0; i < sequences.Length; i++) sequences[i].Initialize();
        }

        public bool IsDone()
        {
            for (int i = 0; i < sequences.Length; i++)
            {
                if (!sequences[i].IsDone() && sequences[i].enabled) return false;
            }
            return true;
        }

        public void SkipSequence()
        {
            if (sequenceStack.Count > 0) sequenceStack.Peek().Stop();
        }

        public void GoToSequence(int index)
        {
            for (int i = 0; i < sequences.Length; i++)
            {
                if (i < index)
                {
                    if (!sequences[i].IsDone()) sequences[i].Stop();
                }
                else break;
            }
            for (int i = index; i < sequences.Length; i++) sequences[i].Initialize();

        }

        public LevelSegment InstantiateSegment()
        {
            while (sequenceStack.Count > 0 && sequenceStack.Peek().IsDone()) sequenceStack.Pop();
            SegmentSequence sequence = null;
            if (sequenceStack.Count == 0)
            {
                sequence = GetSequence();
                if (sequence == null) throw new System.NullReferenceException(title + " has null sequence");
                sequenceStack.Push(sequence);
            }
            sequence = sequenceStack.Peek();
            SegmentDefinition definition = sequence.Next();
            while (definition.nested)
            {
                sequence = definition.nestedSequence;
                sequenceStack.Push(sequence);
                definition = sequence.Next();
            }
            if (sequence != lastSequence)
            {
                if (onSequenceEntered != null) onSequenceEntered(sequence);
                lastSequence = sequence;
            }
            if (definition == null) throw new System.NullReferenceException(title + " has null definition in sequence " + sequence.name);
            return definition.Instantiate();
        }

        private SegmentSequence GetSequence()
        {
            for (int i = 0; i < sequences.Length; i++)
            {
                if (sequences[i].IsDone() && i < sequences.Length - 1) continue;
                if(sequences[i].enabled) return sequences[i];
            }
            return null;
        }

        public Level Duplicate()
        {
            Level level = new Level();
            level.title = title;
            level.remoteSequence = remoteSequence;
            level.remoteSceneName = remoteSceneName;
            level.loadingPriority = loadingPriority;
            level.sequenceCollection = new SegmentSequenceCollection();
            level.sequenceCollection.sequences = new SegmentSequence[sequenceCollection.sequences.Length];
            for (int i = 0; i < sequenceCollection.sequences.Length; i++) level.sequenceCollection.sequences[i] = sequenceCollection.sequences[i].Duplicate();
            return level;
        }
    }
}
