export class AllProduct {
    pageNo: number;
    pageSize: number;
    category: string;
}

export class SearchProduct extends AllProduct {
    searchTerm: string;
}