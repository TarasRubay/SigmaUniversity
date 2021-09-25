using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLList
{
    
        public class DLList<T> : IEnumerable<T>, IEnumerator<T>
        {
            class node
            {
                public node(T data, node pPrev = null, node pNext = null)
                {
                    this.data = data;
                    this.pNext = pNext;
                    this.pPrev = pPrev;
                }
                public T data { get; init; }
                public node pNext { get; set; }
                public node pPrev { get; set; }
            }
            private node first;
            private node last;
            public int Length { get; private set; }
            public T First => first.data;
            public T Last => last.data;
            public DLList()
            {
                Length = 0;
                first = null;
                last = null;
                currentNoda = first;
                positionIndex = 0;
            }

            object IEnumerator.Current => positionIndex;
            private int positionIndex;
            public T Current => currentNoda.data;
            private node currentNoda;

            public void PushFront(T data)
            {
                if (first is null)
                {
                    first = new(data);
                    last = first;
                    Length++;
                    currentNoda = first;
                }
                else
                {
                    node tmp = first;
                    first = new(data, pNext: first);
                    tmp.pPrev = first;
                    Length++;
                    currentNoda = first;
                }
            }
            public void PopFront()
            {
                //тут в CLR зачишчати память та руйнувати обєкт не потрібно це зробить (збірник сміття)
                if (first is not null && last is not null)
                    if (first.pPrev is null && first.pNext is null)
                    {
                        first = null;
                        last = first;
                        currentNoda = first;
                        Length--;
                    }
                    else
                    {

                        first = first.pNext;
                        first.pPrev = null;
                        currentNoda = first;
                        Length--;
                    }
            }
            public void PushBack(T data)
            {
                if (last is null)
                {
                    last = new(data);
                    first = last;
                    Length++;
                    currentNoda = last;
                }
                else
                {
                    node tmp = last;
                    last = new(data, pPrev: last);
                    tmp.pNext = last;
                    Length++;
                    currentNoda = first;
                }
            }
            public void PopBack()
            {
                //тут в CLR зачишчати память та руйнувати обєкт не потрібно це зробить (збірник сміття)
                if (first is not null && last is not null)
                    if (first.pPrev is null && first.pNext is null)
                    {
                        last = null;
                        first = last;
                        currentNoda = first;
                        Length--;
                    }
                    else
                    {
                        last = last.pPrev;
                        last.pNext = null;
                        currentNoda = first;
                        Length--;
                    }
            }
            public void Remove(T data)
            {
                node tmp = first;
                if (first.data.Equals(data)) PopFront();
                else if (data.Equals(last.data)) PopBack();
                else while (tmp.pNext != null)
                    {
                        if (tmp.data.Equals(data))
                        {
                            tmp.pPrev.pNext = tmp.pNext;
                            tmp.pNext.pPrev = tmp.pPrev;
                            Length--;
                            break;
                        }
                        else
                        {
                            tmp = tmp.pNext;
                        }
                    }
            }
            public bool MoveNext()
            {
                if (first == null || last == null)
                {
                    return false;
                }
                else if (positionIndex == 0)
                {
                    positionIndex++;
                    return true;
                }
                else if (currentNoda.pNext is not null)
                {
                    currentNoda = currentNoda.pNext;
                    positionIndex++;
                    return true;
                }
                return false;
            }

            public T this[int index]
            {
                get
                {
                    if (first == null || last == null)
                    {
                        throw new NullReferenceException();
                    }

                    if (index < Length / 2)
                    {
                        if (index < 0 || index > Length - 1) throw new ArgumentOutOfRangeException();
                        node tmp = first;
                        int ind = 0;
                        if (tmp.pNext is null) return tmp.data;
                        while (tmp.pNext != null)
                        {
                            if (index == ind)
                            {
                                return tmp.data;
                            }
                            else
                            {
                                tmp = tmp.pNext;
                                ind++;
                            }
                        }
                        throw new Exception("it is unrealistic to come here");
                    }
                    else
                    {
                        if (index < 0 || index > Length - 1) throw new ArgumentOutOfRangeException();
                        node tmp = last;
                        int ind = Length - 1;
                        if (tmp.pPrev is null) return tmp.data;
                        while (tmp.pPrev != null)
                        {
                            if (index == ind)
                            {
                                return tmp.data;
                            }
                            else
                            {
                                tmp = tmp.pPrev;
                                ind--;
                            }
                        }
                        throw new Exception("it is unrealistic to come here");
                    }
                }
                set
                {
                    if (first == null || last == null)
                    {
                        throw new NullReferenceException();
                    }
                    if (index < 0 || index > Length - 1) throw new ArgumentOutOfRangeException();
                    node tmp = first;
                    int ind = 0;
                    if (tmp.pNext is null)
                    {
                        PushBack(value);
                        Remove(tmp.data);
                    }
                    else while (tmp.pNext != null)
                        {
                            if (index == ind)
                            {
                                InsertAfter(tmp.pPrev.data, value);
                                Remove(tmp.data);
                                break;
                            }
                            else
                            {
                                tmp = tmp.pNext;
                                ind++;
                            }
                        }
                }
            }
            public bool Contains(T data)
            {

                if (first == null || last == null)
                {
                    return false;
                }
                else
                {
                    node tmp = first;
                    if (data.Equals(tmp.data)) return true;
                    while (tmp.pNext != null)
                    {
                        if (data.Equals(tmp.data))
                        {
                            return true;
                        }
                        else
                        {
                            tmp = tmp.pNext;
                        }
                    }
                    if (data.Equals(last.data)) return true;

                }

                return false;
            }
            public void InsertAfter(T beforeData, T insertData)
            {
                if (!Contains(beforeData)) throw new Exception("no found beforeData");
                node tmp = first;
                if (tmp.pNext is null) PushBack(insertData);
                else if (beforeData.Equals(last.data)) PushBack(insertData);
                else while (tmp.pNext != null)
                    {
                        if (beforeData.Equals(tmp.data))
                        {
                            node insert = new(insertData, tmp, tmp.pNext);
                            tmp.pNext.pPrev = insert;
                            tmp.pNext = insert;
                            Length++;
                            break;
                        }
                        else
                        {
                            tmp = tmp.pNext;
                        }
                    }
            }
            public void Reset()
            {
                throw new NotImplementedException();
            }// not implemented
            public void Add(T data)
            {
                PushBack(data);
            }
            public void Cleare()
            {
                if (first == null || last == null)
                {
                    throw new NullReferenceException();
                }
                while (first != null)
                {
                    PopBack();
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public IEnumerator<T> GetEnumerator()
            {
                return this;
            }
            public void Dispose()
            {
                currentNoda = first;
                positionIndex = 0;
            }
        }
}

