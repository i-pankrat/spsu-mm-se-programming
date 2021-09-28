#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <math.h>



int UserInput(double *px, double *py, double *pz)
{
	int operation;
	do
	{
		printf("\nEnter the natural x value, please.\nX = ");
		operation = scanf("%lf", px);
	} 
	while (CheckErrors(px, &operation));
	do
	{
		printf("\nEnter the natural y value, please.\nY = ");
		operation = scanf("%lf", py);
	}
	while (CheckErrors(py, &operation));
	do
	{
		printf("\nEnter the natural z value, please.\nZ = ");
		operation = scanf("%lf", pz);
	} 
	while (CheckErrors(pz, &operation));
	return *px, *py, *pz;
}
int CheckErrors(double *value, int *operation)
{
	if (*operation != 1 || *value <= 0 || *value - (int)*value != 0)
	{
		fseek(stdin, 0, 0);
		printf("\nThe entered value was not the expected one.\n");
		return 1;
	}
	else
	{
		return 0;
	}
}
int gcd(double* px, double *py, double* pz)
{
	int count = 0;
	for (int coefficient = 2; coefficient <= min(min(*px, *py), *pz); coefficient++)
	{
		if((int)*px % coefficient == 0 && (int)*py % coefficient == 0 && (int)*pz % coefficient == 0)
		{
			return 0;
		}
		else
		{
			count++;
		}
	}
	if (count == min(min(*px, *py), *pz) - 1)
	{

		return 1;
	}
}
void main(void)
{ 
	double x, y, z;
	printf("Enter the natural numbers x, y, z to check whether they are a Pythagorean triple (x^2 + y^2 = z^2).\nThe programm will also show whether they are a primitive Pythagorean triple.\n");
	UserInput(&x, &y, &z);
	if (pow(x, 2) + pow(y, 2) == pow(z, 2))
	{
		if (gcd(&x, &y, &z) == 1)
		{
			printf("\n>>>The natural numbers %d, %d, %d are a primitive Pythagorean triple.<<<", (int)x, (int)y, (int)z);
		}
		else
		{
			printf("\n>>>The natural numbers %d, %d, %d are a Pythagorean triple, but not a primitive one.<<<", (int)x, (int)y, (int)z);
		}
	}
	else
	{
		printf("\n>>>The natural numbers %d, %d, %d are not a Pythagorean triple.<<<", (int)x, (int)y, (int)z);
	}
	printf("\n-Thank you for using this programm.\n");
}
