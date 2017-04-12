//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
namespace Com.Visualed.Infra.Storage
{
    public interface IOpSaver
    {
        void Save<T>(T _options) where T : class;

        T InitFromFile<T>() where T : class;
    }

    public interface IOpWrapper<T> where T : class
    {
        T Option { get; set; }
    }
}