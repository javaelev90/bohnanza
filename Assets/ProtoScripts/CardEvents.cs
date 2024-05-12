using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CardReleasedEvent : Event
{

}

public class CardPressedEvent : Event
{

}

public class CardInteractionEvent : Event
{
    public CardV2 Card { get; set; }
}
