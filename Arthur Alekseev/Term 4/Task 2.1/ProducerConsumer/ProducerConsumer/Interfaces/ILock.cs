﻿namespace ProducerConsumer.Interfaces;

public interface ILock
{
	public void Lock();
	public void Unlock();
}