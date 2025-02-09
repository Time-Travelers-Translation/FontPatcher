﻿namespace Logic.Business.FontPatcher.InternalContract
{
    public interface ICharacterProvider
    {
        IList<char> GetAll();
        bool TryGet(char originalCharacter, out char mappedCharacter);
    }
}
