export class Data {
  constructor() {
    this.totalDays = 365;
    this.reinvestingDays = 200;
    this.initialPacks = 100;
    this.returnPerDayPerPack = 0.185;
    this.exchangeRate = 4.5;
  }

  public totalDays: number;
  public reinvestingDays: number;
  public initialPacks: number;
  public returnPerDayPerPack: number;
  public exchangeRate: number;
  public maxActivePacks: string;
  public activePacks: string;
  public total: string;
  public initialInvestment: string;
  public profit: string;
  public peakReturnPerDay: string;
  public peakReturnPerMonth: string;
  public profitPercent: string;
  public totalROL: string;
  public initialInvestmentROL: string;
  public profitROL: string;
  public peakReturnPerDayROL: string;
  public peakReturnPerMonthROL: string;
  public totalPercent: string;
  public timer1: string;
  public timer2: string;
  public timer3: string;
}
