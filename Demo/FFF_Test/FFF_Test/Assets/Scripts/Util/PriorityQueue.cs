using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PriorityQueue<T> where T : IComparable<T> {

	// Sources:
	// - http://interactivepython.org/runestone/static/pythonds/Trees/BinaryHeapImplementation.html

	// TODO: Add functionality for choosing min/max priority queue (min/max heap)

	// Variables
	private List<T> heapList;
	private int size;

	// Getter/Setters
	public int Size {
		get {
			return size;
		}
	}

	// Constructor(s)
	// Create a new empty priority queue
	public PriorityQueue() {

		// Initialize 'heapList'
		heapList = new List<T>();
		heapList.Add(default(T));

	}

	// Create a priority queue from Array/List
	public PriorityQueue(List<T> list) {

		// Initialize 'heapList'
		heapList = new List<T>();
		heapList.Add(default(T));

		// Create heap from list
		CreateHeap(list);

	}

	// Destructor
	~PriorityQueue() {
		//...
	}

	public void CreateHeap(List<T> list) {

		// Can be done faster?
		for (int i = 0; i < list.Count; i++) {
			Insert(list[i]);
		}

	}

	// Insert item in heap
	public void Insert(T item) {

		heapList.Add(item);
		size++;
		siftUp(size);

	}

	// Remove and return top item in heap
	public T Pop() {

		T temp = heapList[1];
		heapList[1] = heapList[size];
		size--;
		siftDown();
		return temp;

	}

	// Return value of top item in heap
	public T Peek() {

		return heapList[1];

	}

	// Go from bottom until the element is greater than its parent (or smaller if max-heap)
	private void siftUp(int n) {
		while (heapList[n].CompareTo(heapList[n/2]) < 0 && n/2 != 0) {
			// Swap
			swap(n, n/2);
			n = n/2;
		}
	}

	// Go from root downwards until the element is smaller than both of its children (or greater if max-heap)
	private void siftDown() {
		int n = 1;
		// Must check left and right (not only parent like in siftUp)
		// TODO: Handle null value
		while (((heapList[n].CompareTo(heapList[n*2]) >= 0 && n*2 < size) || (heapList[n].CompareTo(heapList[n*2+1]) >= 0 && n*2+1 < size))) {
			if (heapList[n].CompareTo(heapList[n*2]) >= 0) {
				// Swap
				swap(n, n*2);
				n = n*2;
			}
			else if (heapList[n].CompareTo(heapList[n*2]) >= 0) {
				// Swap
				swap(n, n*2+1);
				n = n*2+1;
			}
			// Else -> Something went wrong!
		}

		/*
		while ((heapList[n] >= heapList[n*2] || heapList[n] >= heapList[n*2+1])) {
			if (heapList[n] >= heapList[n*2]) {
				// Swap
				swap(n, n*2);
				n = n*2;
			}
			else if (heapList[n] >= heapList[n*2+1]) {
				// Swap
				swap(n, n*2+1);
				n = n*2+1;
			}
			// Else -> Something went wrong!
		}
		*/
	}

	// Swap to positions
	private void swap(int i, int n) {
		T temp = heapList [n];
		heapList[n] = heapList[n/2];
		heapList[n/2] = temp;
	}

}
