public class SubscriptionPlan
{
    public int Id { get; set; }
    public string name { set; get; }
    public string Description { get; set; }
    public decimal Price { get; set; }
	public bool isDeleted { get; set; } = false;

    //public DateTime? EndDate { get; set; } = Id == 1 ? DateTime.Now.AddYears(1) : null;

    //private DateTime? _endDate;

    //public DateTime? EndDate
    //{
    //    get => Id == 1 ? DateTime.Now.AddYears(1) : _endDate;
    //    set => _endDate = value;
    //}

    //public int DurationInDays { get; set; }


    //private int _durationInDays;
    //public int DurationInDays
    //{
    //    get => Id == 1 ? 365 : _durationInDays;
    //    set => _durationInDays = value;
    //}

    //private int _totalNumberOfBids;
    //public int TotalNumberOfBids
    //{
    //    get => Id == 1 ? 6 : _totalNumberOfBids;
    //    set => _totalNumberOfBids = value;
    //}

    //public int RemainingNumberOfBids { get; set; }


    //public SubscriptionPlan()
    //{
    //    RemainingNumberOfBids = TotalNumberOfBids;
    //}

    public int? DurationInDays { get; set; } //nullable 34n lw fi plans mlha4 duration
    public int TotalNumber { get; set; } //of bids for freelancer - of prsting projects for client
    

}