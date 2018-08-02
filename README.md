# FEM_Project
A 2-D FEM Program with Visualization
	

Wenkai Niu





Global Goal
To set up a program to compute 2-D elastic problems on the basis of the prior heat transfer programs.
Step Taken to Modify the Program:
a.	Adding New Element Classes
First of all, I extend the elements class and boundary element class to make sure that the heat transfer elements have been extent to 2-D elastic ability.
Considering KX = F as the general equation for the elastic problems. Different from heat transfer program, this program should have 2 unknown D.O.F.s for each nodes. Therefore, add a new class as:
 
Now, the dimensions of F vector and K vector have been doubled. Correspondingly, the dimension of K matrix has been escalated.
b.	Modifying Ke and Fe
For DNDX has 2*NNPE dimension, Ke has the dimension as 2*(2*NNPE). As the textbook chapter 7, add Ke matrix as below:
 
 
Where, GetE is for us to get the constitutive matrix to compute relation between stress and strain. 
 
Similarly, the Fe has been modified as below. The one dimension of heat transfer has been split into two directions by definition of B vector.
 
c.	Boundary Condition Element Class
For one type of boundaries – displacement boundary, we use penalty method. While for the force boundary, we need compute Fe and override the assemblage.
For displacement B.C., the dimension is 2*NNPE, x-axis and y-axis are recorded separately. The code is shown below:
 
 
For force B.C., things are a bit complex. Using of two vector store force at x-axis and y-axis respectively. Then I override Fe (for B.C.) and assemble into global F vector. The code for these two procedure is shown below.
 
 
d.	GUI, Messages and Events
Design of the GUI is just like the HW#4. The only difference is to make sure to add a couple of listboxes to give the ability for B.C.s input. Also, set default values for B.C.s.
 
The set up node set as below code:
 
Then, set up interpolator to implement the shape functions, as well as set up quadrature rules for the numerical integrals. These two are as same as the corresponding part in HW #4, so no repeat here.
After that, I define the material as Aluminum (Al) and give the elastic modulus and shear modulus.
 

As above mentioned, we need to compute Ke. So Lame constants and Poisson ratio computed from E and G are defined in langmu_function and miu_ function.
Set up boundary element sets as below:
 
Here, for abbreviation, only the code for applying left displacement is shown below. The right ones and the force ones are ignored.
 
Finally, I set boundary element sets to submit into solver.
 
At last, we would get the nodal solutions in one vector like:
U = (U11,U12,U21,U22,…)T
For next processing, the nodal solutions are applied to FEMSolver.
 
e.	Postprocessing
First of all, extent the outputtypes into 2. 
 
Correspondingly, the single display type is slip into two different type in three classes. The codes are shown below:
 
 
The right display value class would not be shown here for abbreviation.
Verifications:
Considering the solution of the elastic governing equations are difficulty to be obtained. I use commercial FEA software ABAQUS to verify my program.
Case 1: one displacement B.C.
Horizontal:
Set right boundary displacement as 2, and the right displacement has been constrained and all forces are equal to zero. And then build up same problem in ABAQUS. The results of this problem is shown below:
 
And the results for ABAQUS case are obtained as:
 
We can see that the results are very close to each other. Despite the difference in plotting method, we can come to the idea that the program has been verified for this case.
Vertical:
Setting top displacement as 2, the others are zeros as same as the previous one. The results are shown are:
 
The results from ABAQUS is:
 
Obviously, the case has been verified
Case 2: one force B.C. with one displace B.C.
Horizontal:
For this case, the plate has been applied a displacement at left edge with a constrained displacement at right edge. The magnitude of the force equals to 2. All others are constrained. The conditions and results are shown below:
 
In ABAQUS, I get results like:
 
We can observe that my program got a very close results with ABAQUS results in terms of maximum value in this case (around 2.50E-10). So this case has been verified.
Vertical:
By applying a force at bottom and a displacement on top, we can easily verify the vertical case. The force has 2 as magnitude while others are equal to zeros.

 
The ABAQUS results are shown below:
 
Same as the horizontal case, this vertical case is verified.
Case 3: B.C. not along any axis
Now I applied a force along 45 degree of x-axis, with x-component as 2 and y-component as 2. And constraint all displacements. The conditions and results are shown below.
 
While the ABAQUS give the results as:
 
The maximum value of x-displacement are different while the relative error is around 20%. The penalty method in my program may be not applied in ABAQUS program, which makes the error is so huge. That is my assumption. Another possibility to cause the error is the smoothing methods are different shown in the above two plots.
Case 4: B.C. not along any axis
By applying a body force along with x-axis, we can verify the function of body force, which is a convenient approach to apply force to all nodes. The conditions and results are shown below:
 
 
The relative of the maximum value is around 14%, but the distribution is almost the same. Therefore, we still can say this case is verified. The reason to cause the error is given in case 3. 
Conclusions:
Generally, the development of this program is successful, according to the above case. The error between this FEM program and commercial software is still hard to dig deeper since the details of the commercial program is not given.
