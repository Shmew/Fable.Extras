# Date

`ArrayBuffer` is used to represent a generic, fixed-length raw binary data buffer.

```fsharp
type Date =
    new () = Date()
    new (ticks: int)
    new (ticks: int64)
    new (value: string)
    new (year: float, month: float, ?date: float, 
         ?hours: float, ?minutes: float, 
         ?seconds: float, ?ms: float)
    new (year: int, month: int, ?date: int, 
         ?hours: int, ?minutes: int, 
         ?seconds: int, ?ms: int )
    new (d: JS.Date)
            
    /// Converts the Date to System.DateTime.
    ///
    /// No runetime cost.
    member AsDateTime () : DateTime

    /// Returns the day of the month (1-31) for the specified date according to local time.
    member GetDate () : int
        
    /// Returns the day of the week (0-6) for the specified date according to local time.
    member GetDay () : DayOfWeek
        
    /// Returns the year (4 digits for 4-digit years) of the specified date according to local time.
    member GetFullYear () : int
        
    /// Returns the hour (0-23) in the specified date according to local time.
    member GetHours () : int
        
    /// Returns the milliseconds (0-999) in the specified date according to local time.
    member GetMilliseconds () : int
        
    /// Returns the minutes (0-59) in the specified date according to local time.
    member GetMinutes () : int
        
    /// Returns the month (0-11) in the specified date according to local time.
    member GetMonth () : int
        
    /// Returns the seconds (0-59) in the specified date according to local time.
    member GetSeconds () : int
        
    /// Returns the numeric value of the specified date as the number of milliseconds since 
    /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
    member GetTime () : int64
        
    /// Returns the numeric value of the specified date as the number of milliseconds since 
    /// January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)
    member GetTimezoneOffset () : int64
        
    /// Returns the day (date) of the month (1-31) in the specified date according to 
    /// universal time.
    member GetUTCDate () : int
        
    /// Returns the day of the week (0-6) in the specified date according to universal time.
    member GetUTCDay () : DayOfWeek
        
    /// Returns the year (4 digits for 4-digit years) in the specified date according to 
    /// universal time.
    member GetUTCFullYear () : int
        
    /// Returns the hours (0-23) in the specified date according to universal time.
    member GetUTCHours () : int
        
    /// Returns the milliseconds (0-999) in the specified date according to universal time.
    member GetUTCMilliseconds () : int
        
    /// Returns the minutes (0-59) in the specified date according to universal time.
    member GetUTCMinutes () : int
        
    /// Returns the month (0-11) in the specified date according to universal time.
    member GetUTCMonth () : int
        
    /// Returns the seconds (0-59) in the specified date according to universal time.
    member GetUTCSeconds () : int
        
    /// Sets the day of the month for a specified date according to local time.
    member SetDate (date: int) : Date
        
    /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
    /// local time.
    member SetFullYear (year: int, ?month: int, ?date: int) : Date
        
    /// Sets the hours for a specified date according to local time.
    member SetHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date
        
    /// Sets the milliseconds for a specified date according to local time.
    member SetMilliseconds (ms: int) : Date
        
    /// Sets the minutes for a specified date according to local time.
    member SetMinutes (min: int, ?sec: int, ?ms: int) : Date
        
    /// Sets the month for a specified date according to local time.
    member SetMonth (month: int, ?date: int) : Date
        
    /// Sets the seconds for a specified date according to local time.
    member SetSeconds (sec: int, ?ms: int) : Date
        
    /// Sets the Date object to the time represented by a number of milliseconds since 
    /// January 1, 1970, 00:00:00 UTC. 
    ///
    /// Use negative numbers for times prior.
    member SetTime (time: int64) : Date
        
    /// Sets the day of the month for a specified date according to universal time.
    member SetUTCDate (date: int) : Date
        
    /// Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
    /// universal time.
    member SetUTCFullYear (year: int, ?month: int, ?date: int) : Date
        
    /// Sets the hour for a specified date according to universal time.
    member SetUTCHours (hours: int, ?min: int, ?sec: int, ?ms: int) : Date
        
    /// Sets the milliseconds for a specified date according to universal time.
    member SetUTCMilliseconds (ms: int) : Date
        
    /// Sets the minutes for a specified date according to universal time.
    member SetUTCMinutes (min: int, ?sec: int, ?ms: int) : Date
        
    /// Sets the month for a specified date according to universal time.
    member SetUTCMonth (month: int, ?date: int) : Date
        
    /// Sets the seconds for a specified date according to universal time.
    member SetUTCSeconds (sec: int, ?ms: int) : Date
        
    /// Returns the "date" portion of the Date as a human-readable string like 'Thu Apr 12 2018'.
    member ToDateString () : string
        
    /// Returns a string representing the Date using toISOString(). 
    /// 
    /// Intended for use by JSON.stringify().
    member ToJSON () : string
        
    /// Returns a string with a locality sensitive representation of the date portion of this 
    /// date based on system settings.
    member ToLocaleDateString () : string
        
    /// Returns a string with a locality-sensitive representation of this date.
    member ToLocaleString () : string
        
    /// Returns a string with a locality-sensitive representation of the time portion of this
    /// date, based on system settings.
    member ToLocaleTimeString () : string
        
    /// Converts a date to a string following the ISO 8601 Extended Format.
    member ToISOString () : string
        
    /// Returns a string representing the specified Date object.
    override ToString () : string
        
    /// Returns the "time" portion of the Date as a human-readable string.
    member ToTimeString () : string
        
    /// Converts a date to a string using the UTC timezone.
    member ToUTCString () : string
        
    /// Returns the primitive value of a Date object.
    member ValueOf () : int64
```

The functions outlined below are located in the `ArrayBuffer` module.

## asDateTime

Converts the Date to System.DateTime.

No runetime cost.

Signature:
```fsharp
(d: Date) -> DateTime
```

## getDate

Returns the day of the month (1-31) for the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getDay

Returns the day of the week (0-6) for the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getFullYear

Returns the year (4 digits for 4-digit years) of the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getHours

Returns the hour (0-23) in the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getMilliseconds

Returns the milliseconds (0-999) in the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getMinutes

Returns the minutes (0-59) in the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getMonth

Returns the month (0-11) in the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getSeconds

Returns the seconds (0-59) in the specified date according to local time.

Signature:
```fsharp
(d: Date) -> int
```

## getTime

Returns the numeric value of the specified date as the number of milliseconds since 
January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)

Signature:
```fsharp
(d: Date) -> int64
```

## getTimezoneOffset

Returns the numeric value of the specified date as the number of milliseconds since 
January 1, 1970, 00:00:00 UTC. (Negative values are returned for prior times.)

Signature:
```fsharp
(d: Date) -> int64
```

## getUTCDate

Returns the day (date) of the month (1-31) in the specified date according to 
universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCDay

Returns the day of the week (0-6) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCFullYear

Returns the year (4 digits for 4-digit years) in the specified date according to 
universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCHours

Returns the hours (0-23) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCMilliseconds

Returns the milliseconds (0-999) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCMinutes

Returns the minutes (0-59) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCMonth

Returns the month (0-11) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## getUTCSeconds

Returns the seconds (0-59) in the specified date according to universal time.

Signature:
```fsharp
(d: Date) -> int
```

## invoke

Like Date.now(), but returns a string value of the date.

Signature:
```fsharp
unit -> string
```

## now

Returns the current time in ticks.

Signature:
```fsharp
unit -> int64
```

## parse

Parses a string representation of a date and returns the ticks.

Signature:
```fsharp
(s: string) -> int64
```

## setDate

Sets the day of the month for a specified date according to local time.

Signature:
```fsharp
(date: int) -> (d: Date) -> Date
```

## setFullYear

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
local time.

Signature:
```fsharp
(year: int) -> (d: Date) -> Date
```

## setFullYearM

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
local time.

Signature:
```fsharp
(year: int) -> (month: int) -> (d: Date) -> Date
```

## setFullYearD

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
local time.

Signature:
```fsharp
(year: int) -> (date: int) -> (d: Date) -> Date
```

## setFullYearMD

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
local time.

Signature:
```fsharp
(year: int) -> (month: int) -> (date: int) -> (d: Date) -> Date
```

## setHours

Sets the hours for a specified date according to local time.

Signature:
```fsharp
(hours: int) -> (d: Date) -> Date
```

## setHoursM

Sets the hours for a specified date according to local time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (d: Date) -> Date
```

## setHoursMS

Sets the hours for a specified date according to local time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (sec: int) -> (d: Date) -> Date
```

## setHoursMSM

Sets the hours for a specified date according to local time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (sec: int) -> (ms: int) -> (d: Date) -> Date
```

## setMilliseconds

Sets the milliseconds for a specified date according to local time.

Signature:
```fsharp
(ms: int) -> (d: Date) -> Date
```

## setMinutes

Sets the minutes for a specified date according to local time.

Signature:
```fsharp
(min: int) -> (d: Date) -> Date
```

## setMinutesS

Sets the minutes for a specified date according to local time.

Signature:
```fsharp
(min: int) -> (sec: int) -> (d: Date) -> Date
```

## setMinutesSM

Sets the minutes for a specified date according to local time.

Signature:
```fsharp
(min: int) -> (sec: int) -> (ms: int) -> (d: Date) -> Date
```

## setMonth

Sets the month for a specified date according to local time.

Signature:
```fsharp
(month: int) -> (d: Date) -> Date
```

## setMonthD

Sets the month for a specified date according to local time.

Signature:
```fsharp
(month: int) -> (date: int) -> (d: Date) -> Date
```

## setSeconds

Sets the seconds for a specified date according to local time.

Signature:
```fsharp
(sec: int) -> (d: Date) -> Date
```

## setSecondsM

Sets the seconds for a specified date according to local time.

Signature:
```fsharp
(sec: int) -> (ms: int) -> (d: Date) -> Date
```

## setTime

Sets the Date object to the time represented by a number of milliseconds since 
January 1, 1970, 00:00:00 UTC. 

Use negative numbers for times prior.

Signature:
```fsharp
(time: int64) -> (d: Date) -> Date
```

## setUTCDate

Sets the day of the month for a specified date according to universal time.

Signature:
```fsharp
(date: int) -> (d: Date) -> Date
```

## setUTCFullYear

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
universal time.

Signature:
```fsharp
(year: int) -> (d: Date) -> Date
```

## setUTCFullYearM

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
universal time.

Signature:
```fsharp
(year: int) -> (month: int) -> (d: Date) -> Date
```

## setUTCFullYearD

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
universal time.

Signature:
```fsharp
(year: int) -> (date: int) -> (d: Date) -> Date
```

## setUTCFullYearMD

Sets the full year (e.g. 4 digits for 4-digit years) for a specified date according to 
universal time.

Signature:
```fsharp
(year: int) -> (month: int) -> (date: int) -> (d: Date) -> Date
```

## setUTCHours

Sets the hour for a specified date according to universal time.

Signature:
```fsharp
(hours: int) -> (d: Date) -> Date
```

## setUTCHoursM

Sets the hour for a specified date according to universal time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (d: Date) -> Date
```

## setUTCHoursMS

Sets the hour for a specified date according to universal time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (sec: int) -> (d: Date) -> Date
```

## setUTCHoursMSM

Sets the hour for a specified date according to universal time.

Signature:
```fsharp
(hours: int) -> (min: int) -> (sec: int) -> (ms: int) -> (d: Date) -> Date
```

## setUTCMilliseconds

Sets the milliseconds for a specified date according to universal time.

Signature:
```fsharp
(ms: int) -> (d: Date) -> Date
```

## setUTCMinutes

Sets the minutes for a specified date according to universal time.

Signature:
```fsharp
(min: int) -> (d: Date) -> Date
```

## setUTCMinutesS

Sets the minutes for a specified date according to universal time.

Signature:
```fsharp
(min: int) -> (sec: int) -> (d: Date) -> Date
```

## setUTCMinutesMS

Sets the minutes for a specified date according to universal time.

Signature:
```fsharp
(min: int) -> (sec: int) -> (ms: int) -> (d: Date) -> Date
```

## setUTCMonth

Sets the month for a specified date according to universal time.

Signature:
```fsharp
(month: int) -> (d: Date) -> Date
```

## setUTCMonthD

Sets the month for a specified date according to universal time.

Signature:
```fsharp
(month: int) -> (date: int) -> (d: Date) -> Date
```

## setUTCSeconds

Sets the seconds for a specified date according to universal time.

Signature:
```fsharp
(sec: int) -> (d: Date) -> Date
```

## setUTCSecondsM

Sets the seconds for a specified date according to universal time.

Signature:
```fsharp
(sec: int) -> (ms: int) -> (d: Date) -> Date
```

## toDateString

Returns the "date" portion of the Date as a human-readable string like 'Thu Apr 12 2018'.

Signature:
```fsharp
(d: Date) -> string
```

## toJSON

Returns a string representing the Date using toISOString(). 

Intended for use by [JSON.stringify](/extras/json#stringify).

Signature:
```fsharp
(d: Date) -> string
```

## toLocaleDateString

Returns a string with a locality sensitive representation of the date portion of this 
date based on system settings.

Signature:
```fsharp
(d: Date) -> string
```

## toLocaleString

Returns a string with a locality-sensitive representation of this date.

Signature:
```fsharp
(d: Date) -> string
```

## toLocaleTimeString

Returns a string with a locality-sensitive representation of the time portion of this
date, based on system settings.

Signature:
```fsharp
(d: Date) -> string
```

## toISOString

Converts a date to a string following the ISO 8601 Extended Format.

Signature:
```fsharp
(d: Date) -> string
```

## toString

Returns a string representing the specified Date object.

Signature:
```fsharp
(d: Date) -> string
```

## toTimeString

Returns the "time" portion of the Date as a human-readable string.

Signature:
```fsharp
(d: Date) -> string
```

## toUTCString

Converts a date to a string using the UTC timezone.

Signature:
```fsharp
(d: Date) -> string
```

## valueOf

Returns the primitive value of a Date object.

Signature:
```fsharp
(d: Date) -> int64
```

### UTC

Accepts parameters similar to the Date constructor, but treats them as UTC 
in ticks.

Signature:
```fsharp
(year: int, month: int, ?date: int, ?hours: int, ?minutes: int, ?seconds: int, ?ms: int) -> int64
```
