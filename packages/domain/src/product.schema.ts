import { z } from "zod";

import { categorySchema } from "./category.schema";
import type { Product } from "./product";

export const createProductSchema = z.object({
  name: z.string().trim().min(1),
  sku: z.string().trim().min(1),
  price: z.number().nonnegative(),
  stockQuantity: z.number().int().nonnegative(),
  categoryId: z.number().int().positive(),
});

export const productSchema: z.ZodType<Product> = createProductSchema
  .extend({
    id: z.number().int().positive(),
    category: z.lazy(() => categorySchema).nullable().optional(),
  })
  .transform(({ category, ...product }) =>
    category === undefined ? product : { ...product, category },
  );
