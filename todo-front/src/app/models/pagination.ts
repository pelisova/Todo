export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult<T> {
  result: Result<T> | undefined;
  pagination: Pagination | undefined;
}

export type Result<T> = {
  message: string | undefined;
  data: ResultPag<T>;
};

export type ResultPag<T> = {
  items: T;
  pagination: Pagination;
};
