export interface TodoTask {
  id: number;
  text: string;
  completed: boolean;
  userId: number;
}

export interface CreateTask {
  text: string;
  userId: number;
}

export interface PagParams {
  pageNumber?: number;
  pageSize?: number;
}
