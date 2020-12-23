using System;
using System.Collections.Generic;
using System.Reflection;

namespace MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions
{
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {
    }

    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; protected set; }

        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if ( EqualityComparer<TPrimaryKey>.Default.Equals( Id, default( TPrimaryKey ) ) )
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if ( typeof( TPrimaryKey ) == typeof( int ) )
            {
                return Convert.ToInt32( Id ) <= 0;
            }

            if ( typeof( TPrimaryKey ) == typeof( long ) )
            {
                return Convert.ToInt64( Id ) <= 0;
            }

            return false;
        }

        public override bool Equals( object obj )
        {
            if ( obj == null || !( obj is Entity<TPrimaryKey> ) )
            {
                return false;
            }

            //Same instances must be considered as equal
            if ( ReferenceEquals( this, obj ) )
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (Entity<TPrimaryKey>) obj;
            if ( IsTransient() && other.IsTransient() )
            {
                return false;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if ( !typeOfThis.GetTypeInfo().IsAssignableFrom( typeOfOther ) && !typeOfOther.GetTypeInfo().IsAssignableFrom( typeOfThis ) )
            {
                return false;
            }

            return Id.Equals( other.Id );
        }

        int? _requestedHashCode;
        public override int GetHashCode()
        {
            if ( !IsTransient() )
            {
                if ( !_requestedHashCode.HasValue )
                    _requestedHashCode =
                        Id.GetHashCode() ^
                        31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }

            return base.GetHashCode();
        }

        public static bool operator ==( Entity<TPrimaryKey> left, Entity<TPrimaryKey> right )
        {
            if ( Equals( left, null ) )
            {
                return Equals( right, null );
            }

            return left.Equals( right );
        }

        public static bool operator !=( Entity<TPrimaryKey> left, Entity<TPrimaryKey> right )
        {
            return !( left == right );
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
