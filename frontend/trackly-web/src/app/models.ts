export interface Product{id:string;sourceId:string;name:string;productUrl:string;imageUrl?:string;category?:string;currentPrice:number;currency:string;availability?:string;rating?:string;lastScrapedAt?:string;createdAt:string;updatedAt:string}
export interface PriceHistory{id:string;productId:string;price:number;currency:string;scrapedAt:string}
export interface Source{id:string;name:string;baseUrl:string;sourceType:string;isActive:boolean;createdAt:string}
export interface ScrapeJob{id:string;sourceId:string;sourceName?:string;status:string;startedAt?:string;finishedAt?:string;productsFound:number;productsUpdated:number;errorMessage?:string}
export interface Summary{totalProducts:number;totalSources:number;latestScrapeStatus?:string;productsWithPriceDrop:number;productsScrapedToday:number}
